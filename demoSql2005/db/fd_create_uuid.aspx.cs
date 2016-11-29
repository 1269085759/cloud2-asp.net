using HttpUploader6.demoSql2005.db.biz;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;
using System.Web;

namespace HttpUploader6.demoSql2005.db
{
    /// <summary>
    /// 以uuid模式存储文件夹，自动生成文件存储路径，自动生成文件夹存储路径
    /// 和客户端文件夹结构完全保持一致。
    /// </summary>
    public partial class fd_create_uuid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string folderStr = Request.Form["folder"];
            string uidTxt = Request.Form["uid"];
            int uid = int.Parse(uidTxt);
            if (string.IsNullOrEmpty(folderStr)) return;
            folderStr = HttpUtility.UrlDecode(folderStr);

            //文件夹ID，文件夹对象
            Hashtable tbFolders = new Hashtable();

            JObject jsonObj = JObject.Parse(folderStr);
            FolderInf fdroot = JsonConvert.DeserializeObject<FolderInf>(folderStr);
            fdroot.pathRel = fdroot.nameLoc;//相对路径

            PathUuidBuilder pb = new PathUuidBuilder();
            fdroot.pathSvr = pb.genFolder(uid, ref fdroot);
            if (!Directory.Exists(fdroot.pathSvr)) Directory.CreateDirectory(fdroot.pathSvr);

            fdroot.idSvr = DBFolder.Add(ref fdroot);//添加到数据库
            fdroot.idFile = DBFile.Add(ref fdroot);//向文件表添加一条数据
            tbFolders.Add(0, fdroot);//提供给子文件夹使用

            //解析文件夹
            JArray arrFolders = new JArray();
            if (jsonObj["folders"] != null)
            {
                JArray jar = JArray.Parse(jsonObj["folders"].ToString());
                for (int i = 0, l = jar.Count; i < l; ++i)
                {
                    folderStr = jar[i].ToString();//把每一个元素转化为JObject对象
                    FolderInf folder = JsonConvert.DeserializeObject<FolderInf>(folderStr);
                    folder.uid = uid;
                    folder.pidRoot = fdroot.idSvr;

                    //创建层级结构
                    FolderInf fdParent = (FolderInf)tbFolders[folder.m_pidLoc];
                    folder.pathSvr = Path.Combine(fdParent.pathSvr, folder.nameLoc);
                    folder.pathRel = Path.Combine(fdParent.pathRel, folder.nameLoc);
                    folder.m_pidSvr = fdParent.m_idSvr;
                    folder.m_idSvr = DBFolder.Add(ref folder);//添加到数据库
                    tbFolders.Add(folder.m_idLoc, folder);
                    arrFolders.Add(JToken.FromObject(folder));
                }
            }

            //解析文件
            JArray arrFiles = new JArray();
            if (jsonObj["files"] != null)
            {
                JArray jar = JArray.Parse(jsonObj["files"].ToString());
                for (int i = 0, l = jar.Count; i < l; ++i)
                {
                    folderStr = jar[i].ToString();//把每一个元素转化为JObject对象
                    FileInf file = JsonConvert.DeserializeObject<FileInf>(folderStr);
                    FolderInf pidFd = (FolderInf)tbFolders[file.pidLoc];
                    file.uid = uid;
                    file.pidRoot = fdroot.m_idSvr;
                    file.pidSvr = pidFd.idSvr;
                    file.nameSvr = file.nameLoc;
                    //以层级结构存储
                    file.pathSvr = Path.Combine(pidFd.pathSvr, file.nameLoc);
                    file.idSvr = DBFile.Add(ref file);//将信息添加到数据库
                    arrFiles.Add(JToken.FromObject(file));
                }
            }

            //转换为JSON
            JObject obj = (JObject)JToken.FromObject(fdroot);
            obj["folders"] = arrFolders;
            obj["files"] = arrFiles;
            obj["complete"] = false;

            string json = obj.ToString();
            json = HttpUtility.UrlEncode(json);
            //UrlEncode会将空格解析成+号，
            json = json.Replace("+", "%20");
            Response.Write(json);
        }
    }
}
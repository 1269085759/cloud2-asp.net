using System;
using System.Web;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.IO;
using HttpUploader6.demoSql2005.db.biz;

namespace HttpUploader6.demoSql2005.db
{
    /// <summary>
    /// 以md5模式存储文件夹，
    /// 
    /// 创建文件夹，并返回创建成功后的文件夹信息。
    /// 1.接收客户端传来的文件夹JSON信息
    /// 2.解析文件夹JSON信息，并根据层级关系创建子文件夹
    /// 3.将文件夹信息保存到数据库
    /// 4.更新文件夹JSON信息并返回到客户端。
    /// 将文件夹JSON保存到文件夹数据表中
    /// </summary>
    public partial class fd_create : System.Web.UI.Page
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
            JArray arrFolders = new JArray();
            JArray arrFiles = new JArray();

            JObject jsonObj = JObject.Parse(folderStr);
            FolderInf fdroot = JsonConvert.DeserializeObject<FolderInf>(folderStr);
            fdroot.pathRel = fdroot.nameLoc;//相对路径

            fdroot.m_idSvr = DBFolder.Add(ref fdroot);//添加到数据库
            fdroot.idFile = DBFile.Add(ref fdroot);//向文件表添加一条数据
            tbFolders.Add(0, fdroot);//提供给子文件夹使用

            //解析文件夹
            if (jsonObj["folders"]!=null)
            {
                JArray jar = JArray.Parse(jsonObj["folders"].ToString());
                for (int i = 0, l = jar.Count; i < l; ++i)
                {
                    folderStr = jar[i].ToString();//把每一个元素转化为JObject对象
                    FolderInf folder = JsonConvert.DeserializeObject<FolderInf>(folderStr);
                    folder.uid = uid;
                    folder.pidRoot = fdroot.idSvr;
                    //查找父级文件夹
                    FolderInf fdParent = (FolderInf)tbFolders[folder.m_pidLoc];
                    folder.pathRel = Path.Combine( fdParent.pathRel , folder.nameLoc);
                    folder.m_pidSvr = fdParent.m_idSvr;
                    folder.m_idSvr = DBFolder.Add(ref folder);//添加到数据库
                    tbFolders.Add(folder.m_idLoc, folder);
                    arrFolders.Add(JToken.FromObject(folder));
                }
            }

            DBFile db = new DBFile();
            xdb_files f_exist = new xdb_files();
            //解析文件
            if (jsonObj["files"]!=null)
            {
                JArray jar = JArray.Parse(jsonObj["files"].ToString());
                for (int i = 0, l = jar.Count; i < l; ++i)
                {
                    folderStr = jar[i].ToString();//把每一个元素转化为JObject对象
                    FileInf fileSvr = JsonConvert.DeserializeObject<FileInf>(folderStr);
                    FolderInf folder = (FolderInf)tbFolders[fileSvr.pidLoc];
                    fileSvr.uid = uid;
                    fileSvr.pidRoot = fdroot.m_idSvr;
                    fileSvr.pidSvr = folder.idSvr;
                    fileSvr.nameSvr = fileSvr.md5 + Path.GetExtension(fileSvr.pathLoc).ToLower();
                    //生成文件路径
                    var  pb = new PathCloudBuilder();
                    fileSvr.pathSvr = pb.genFile(fileSvr.uid, fileSvr.md5,fileSvr.nameLoc);

                    //存在相同文件
                    if (db.exist_file(fileSvr.md5,ref f_exist))
                    {
                        fileSvr.lenSvr = f_exist.lenSvr;
                        fileSvr.perSvr = f_exist.perSvr;
                        fileSvr.pathSvr = f_exist.pathSvr;
                        fileSvr.pathRel = f_exist.pathRel;
                        fileSvr.postPos = f_exist.FilePos;
                        fileSvr.complete = f_exist.complete;
                        fileSvr.nameSvr = f_exist.nameSvr;
                    }
                    fileSvr.idSvr = DBFile.Add(ref fileSvr);//将信息添加到数据库
                    arrFiles.Add(JToken.FromObject(fileSvr));
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
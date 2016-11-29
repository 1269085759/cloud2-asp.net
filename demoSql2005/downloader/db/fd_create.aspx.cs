using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HttpUploader6.demoSql2005.downloader.db
{
    /// <summary>
    /// 创建一个文件夹下载任务。
    /// </summary>
    public partial class fd_create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uid      = Request.QueryString["uid"];
            string fdID     = Request.QueryString["fdid"];//xdb_folders.fd_id
            string mac      = Request.QueryString["mac"];
            string name     = Request.QueryString["name"];
            string pathLoc  = Request.QueryString["pathLoc"];//客户端使用的是encodeURIComponent编码，
            string cbk      = Request.QueryString["callback"];//应用于jsonp数据

            if (string.IsNullOrEmpty(uid)
               || string.IsNullOrEmpty(mac)
               || string.IsNullOrEmpty(pathLoc)
               || string.IsNullOrEmpty(name))
            {
                Response.Write("参数为空");
                Response.End();
                return;
            }

            DnFolderInf df = new DnFolderInf();
            df.uid = int.Parse(uid);
            df.mac = mac;
            df.name = name;
            df.pathLoc = pathLoc;
            df.fdID = int.Parse(fdID);
            df.idSvr = DnFolder.Add(ref df);

            //添加到down_files表
            df.idF = DnFile.Add(ref df);

            string json = JsonConvert.SerializeObject(df);
            json = HttpUtility.UrlEncode(json);
            //UrlEncode会将空格解析成+号
            json = json.Replace("+", "%20");
            json = cbk + "(" + json + ")";//返回jsonp格式数据。
            Response.Write(json);
        }
    }
}
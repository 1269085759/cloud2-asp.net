using HttpUploader6.demoSql2005.db;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HttpUploader6.demoSql2005.downloader.db
{
    public partial class fd_json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fdID     = Request.QueryString["fdID"];//xdb_folders.fd_id
            string idSvr    = Request.QueryString["idSvr"];
            string cbk      = Request.QueryString["callback"];

            FolderInf fi = new FolderInf();
            string json = string.Empty;
            if (!string.IsNullOrEmpty(idSvr))
            {
                json = DnFolder.GetFolderData(int.Parse(idSvr), ref fi);
            }
            else if (!string.IsNullOrEmpty(fdID))
            {
                json = DBFolder.GetFolderData(int.Parse(fdID), ref fi);
            }

            json = HttpUtility.UrlEncode(json);
            //UrlEncode会将空格解析成+号
            json = json.Replace("+", "%20");
            //Response.Write(cbk+"("+json+ ")");
            Response.Write(json);
        }
    }
}
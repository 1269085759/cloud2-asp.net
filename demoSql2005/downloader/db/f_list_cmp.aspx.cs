using HttpUploader6.demoSql2005.db;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HttpUploader6.demoSql2005.downloader.db
{
    /// <summary>
    /// 列出所有已经上传完的文件和文件夹
    /// </summary>
    public partial class f_list_cmp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uid = Request.QueryString["uid"];
            string cbk = Request.QueryString["callback"];//jsonp

            if (!string.IsNullOrEmpty(uid))
            {
                string json = DBFile.GetAllComplete(int.Parse(uid));
                //json = json.Replace("\\r\\n", string.Empty);
                json = HttpUtility.UrlEncode(json);
                //UrlEncode会将空格解析成+号
                json = json.Replace("+", "%20");
                Response.Write(json);
                //Response.Write(callback + "(" + json + ")");
            }
        }
    }
}
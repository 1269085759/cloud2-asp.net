using System;

using System.Web;

namespace HttpUploader6.demoSql2005.downloader.db
{
    /// <summary>
    /// 列出未完成的文件和文件夹下载任务。
    /// </summary>
    public partial class d_list : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uid = Request.QueryString["uid"];
            string mac = Request.QueryString["mac"];

            if (string.IsNullOrEmpty(uid)
                || string.IsNullOrEmpty(mac))
            {
                Response.Write("参数为空");
                Response.End();
                return;
            }

            string json = DnFile.GetAll(int.Parse(uid), mac);
            json = HttpUtility.UrlEncode(json);
            json = json.Replace("+", "%20");

            Response.Write(json);
        }
    }
}
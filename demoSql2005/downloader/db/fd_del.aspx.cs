using System;

namespace HttpUploader6.demoSql2005.downloader.db
{
    /// <summary>
    /// 删除文件夹下载任务。
    /// </summary>
    public partial class fd_del : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idF  = Request.QueryString["idF"];
            string idFD = Request.QueryString["idFD"];
            string mac  = Request.QueryString["mac"];
            string uid  = Request.QueryString["uid"];

            if (!string.IsNullOrEmpty(idF)
                && !string.IsNullOrEmpty(idFD)
                && !string.IsNullOrEmpty(mac)
                && !string.IsNullOrEmpty(uid))
            {
                DnFolder.Del(idF, idFD, uid, mac);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HttpUploader6.demoSql2005.downloader.db
{
    /// <summary>
    /// 更新文件夹下载进度。
    /// </summary>
    public partial class fd_update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fid = Request.QueryString["fid"];
            string uid = Request.QueryString["uid"];
            string mac = Request.QueryString["mac"];
            string per = Request.QueryString["percent"];

            if (!string.IsNullOrEmpty(fid)
                && !string.IsNullOrEmpty(uid)
                && !string.IsNullOrEmpty(mac)
                && !string.IsNullOrEmpty(per)
                )
            {
                DnFolder.Update(fid, uid, mac, per);
            }
        }
    }
}
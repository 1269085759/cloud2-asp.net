using System;

namespace HttpUploader6.demoSql2005.downloader.db
{
    /// <summary>
    /// 更新文件下载进度
    /// </summary>
    public partial class f_update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fid      = Request.QueryString["fid"];
            string uid      = Request.QueryString["uid"];
            string mac      = Request.QueryString["mac"];
            string per      = Request.QueryString["percent"];
            string lenLoc   = Request.QueryString["lenLoc"];
            string cbk      = Request.QueryString["callback"];

            if (string.IsNullOrEmpty(uid)
                || string.IsNullOrEmpty(fid)
                || string.IsNullOrEmpty(cbk)
                || string.IsNullOrEmpty(lenLoc))
            {
                Response.Write("参数为空");
                Response.End();
                return;
            }

            DnFile db = new DnFile();
            db.UpdateLengthLoc(int.Parse(fid), int.Parse(uid), mac, lenLoc, per);
            Response.Write(cbk + "(1)");
        }
    }
}
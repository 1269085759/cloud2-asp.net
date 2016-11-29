using System;

namespace HttpUploader6.demoSql2005.downloader.db
{
    public partial class f_del : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fid = Request.QueryString["fid"];
            string uid = Request.QueryString["uid"];
            string mac = Request.QueryString["mac"];
            string cbk = Request.QueryString["callback"];

            if (string.IsNullOrEmpty(uid)
                || string.IsNullOrEmpty(fid)
                || string.IsNullOrEmpty(mac))
            {
                Response.Write(cbk + "(0)");
                return;
            }

            DnFile db = new DnFile();
            db.Delete(int.Parse(fid), int.Parse(uid), mac);

            Response.Write(cbk + "(1)");
        }
    }
}
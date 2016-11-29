using System;

namespace HttpUploader6.demoSql2005.db
{
    public partial class fd_complete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id_file = Request.QueryString["id_file"];
            string id_fd = Request.QueryString["id_folder"];
            string uid = Request.QueryString["uid"];
            string callback = Request.QueryString["callback"];
            int ret = 0;

            if (string.IsNullOrEmpty(id_fd)
                || string.IsNullOrEmpty(uid))
            {
            }
            else
            {
                DBFolder.Complete(int.Parse(id_fd), int.Parse(uid));
                DBFile.fd_complete(int.Parse(id_file));
                ret = 1;
            }
            Response.Write(callback + "(" + ret + ")");
        }
    }
}
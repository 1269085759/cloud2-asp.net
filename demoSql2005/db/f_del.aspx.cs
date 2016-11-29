using System;

namespace HttpUploader6.demoSql2005.db
{
    public partial class f_del : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fid = Request.QueryString["fid"];
            string uid = Request.QueryString["uid"];
            string callback = Request.QueryString["callback"];
            int ret = 0;

            if (string.IsNullOrEmpty(fid)
                || string.IsNullOrEmpty(uid))
            {
            }//参数不为空
            else
            {
                DBFile db = new DBFile();
                db.Delete(Convert.ToInt32(uid), Convert.ToInt32(fid));
                ret = 1;
            }
            Response.Write(callback + "(" + ret + ")");//返回jsonp格式数据
        }
    }
}
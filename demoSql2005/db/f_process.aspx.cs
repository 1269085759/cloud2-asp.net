using System;

namespace HttpUploader6.demoSql2005.db
{
    /// <summary>
    /// 此页面更新文件上传进度。
    /// 在文件上传错误和停止上传时更新。
    /// </summary>
    public partial class f_process : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uid      = Request.QueryString["uid"];
            string idSvr    = Request.QueryString["idSvr"];
            string md5      = Request.QueryString["md5"];
            string perSvr   = Request.QueryString["perSvr"];//文件百分比
            string lenSvr   = Request.QueryString["lenSvr"];//已传大小
            string lenLoc   = Request.QueryString["lenLoc"];//本地文件大小
            string cbk      = Request.QueryString["callback"];

            //参数为空
            if (string.IsNullOrEmpty(lenLoc)
                || string.IsNullOrEmpty(uid)
                || string.IsNullOrEmpty(idSvr)
                || string.IsNullOrEmpty(md5)
                )
            {
                XDebug.Output("lenLoc", lenLoc);
                XDebug.Output("uid", uid);
                XDebug.Output("idSvr", idSvr);
                XDebug.Output("md5", md5);

                Response.Write(cbk + "({\"value\":null,\"des\":\"param is null\"})");
                return;
            }

            XDebug.Output("lenLoc", lenLoc);
            XDebug.Output("uid", uid);
            XDebug.Output("idSvr", idSvr);
            XDebug.Output("lenSvr", lenSvr);
            XDebug.Output("perSvr", perSvr);

            DBFile db = new DBFile();
            db.UpdateProgress(Convert.ToInt32(uid), Convert.ToInt32(idSvr), 0, Convert.ToInt64(lenSvr), perSvr);
            Response.Write(cbk + "({\"value\":true})");
        }
    }
}
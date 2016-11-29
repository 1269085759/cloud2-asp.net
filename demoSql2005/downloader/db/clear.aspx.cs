using System;

namespace HttpUploader6.demoSql2005.downloader.db
{
    public partial class clear : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DnFile.Clear();
            DnFolder.Clear();
        }
    }
}
using System;

namespace HttpUploader6.demoSql2005.db
{
    public partial class clear : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBFile.Clear();
            DBFolder.Clear();
        }
    }
}
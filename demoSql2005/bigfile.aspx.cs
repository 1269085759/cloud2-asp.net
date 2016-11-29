using System;
using System.IO;

namespace HttpUploaderWeb.demoSql2005
{
    public partial class bigfile : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			FileStream fs = File.OpenWrite("d:\\test.db");
			BinaryWriter w = new BinaryWriter(fs);
            long file_size = 6442450944;
            file_size = 10371208;
            Random ra = new Random();

            w.Write((byte)ra.Next());//
            for (long i = 1; i < file_size; ++i)
			{
				w.Write((byte)0);
			}
			w.Close();
			fs.Close();
		}
	}
}
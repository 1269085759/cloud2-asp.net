using System;
using System.IO;
using System.Text;
using System.Web;

namespace HttpUploader6.demoSql2005.downloader.db
{
    public partial class f_down : System.Web.UI.Page
    {
        /// <summary>
        /// 为字符串中的非英文字符编码Encodes non-US-ASCII characters in a string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToHexString(string s)
        {
            char[] chars = s.ToCharArray();
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < chars.Length; index++)
            {
                bool needToEncode = NeedToEncode(chars[index]);
                if (needToEncode)
                {
                    string encodedString = ToHexString(chars[index]);
                    builder.Append(encodedString);
                }
                else
                {
                    builder.Append(chars[index]);
                }
            }
            return builder.ToString();
        }
        /// <summary>
        ///指定一个字符是否应该被编码 Determines if the character needs to be encoded.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static bool NeedToEncode(char chr)
        {
            string reservedChars = "$-_.+!*'(),@=&";
            if (chr > 127)
                return true;
            if (char.IsLetterOrDigit(chr) || reservedChars.IndexOf(chr) >= 0)
                return false;
            return true;
        }
        /// <summary>
        /// 为非英文字符串编码Encodes a non-US-ASCII character.
        /// </summary>
        /// <param name="chr"></param>
        /// <returns></returns>
        private static string ToHexString(char chr)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(chr.ToString());
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < encodedBytes.Length; index++)
            {
                builder.AppendFormat("%{0}", Convert.ToString(encodedBytes[index], 16));
            }
            return builder.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string fid = Request.QueryString["fid"];
            if (String.IsNullOrEmpty(fid))
            {
                return;
            }
            demoSql2005.db.DBFile f = new demoSql2005.db.DBFile();
            demoSql2005.db.xdb_files inf = new demoSql2005.db.xdb_files();
            //数据库不存在文件
            if (!f.GetFileInfByFid(int.Parse(fid), ref inf))
            {
                return;
            }
            string nameLoc = Path.GetFileName(inf.nameLoc);
            string ext = Path.GetExtension(nameLoc);
            string encodefileName = ToHexString(nameLoc);//使用自定义的
            if (Request.Browser.Browser.Contains("IE"))
            {
                string name = encodefileName.Remove(encodefileName.Length - ext.Length);//得到文件名称
                name = name.Replace(".", "%2e"); //关键代码
                encodefileName = name + ext;
            }
            string pathSvr = inf.pathSvr;

            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] encodedBytes = utf8.GetBytes(nameLoc);

            string fnUtf8 = Encoding.UTF8.GetString(Encoding.GetEncoding("gb2312").GetBytes(nameLoc));
            fnUtf8 = HttpUtility.UrlEncode(nameLoc);

            Stream iStream = null;
            try
            {
                // Open the file.
                iStream = new System.IO.FileStream(pathSvr, FileMode.Open, FileAccess.Read, FileShare.Read);

                // Total bytes to read:
                long dataToRead = iStream.Length;

                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fnUtf8 + "\"");
                string range = Request.Headers.Get("Range");//续传
                if (!string.IsNullOrEmpty(range))
                {
                    string[] rs = range.Split("=".ToCharArray());//bytes=10254-lenTotal
                    var lenArr = rs[1].Split("-".ToCharArray());//
                    var lenCur = long.Parse(lenArr[0]);
                    iStream.Seek(lenCur, SeekOrigin.Begin);
                    dataToRead -= lenCur;//fix(2015-08-12):修复返回长度不正确的问题。
                }
                Response.AddHeader("Content-Length", dataToRead.ToString());

                byte[] buffer = new Byte[10000];
                int length;
                // Read the bytes.
                while (dataToRead > 0)
                {
                    // Verify that the client is connected.
                    if (Response.IsClientConnected)
                    {
                        // Read the data in buffer.
                        length = iStream.Read(buffer, 0, 10000);

                        // Write the data to the current output stream.
                        Response.OutputStream.Write(buffer, 0, length);

                        // Flush the data to the HTML output.
                        Response.Flush();

                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        //prevent infinite loop if user disconnects
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                // Trap the error, if any.
                Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    //Close the file.
                    iStream.Close();
                }
            }

        }
    }
}
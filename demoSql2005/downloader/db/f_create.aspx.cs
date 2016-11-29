using Newtonsoft.Json;
using System;
using System.Web;

namespace HttpUploader6.demoSql2005.downloader.db
{
    public partial class f_create : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uid          = Request.QueryString["uid"];
            string mac          = Request.QueryString["mac"];
            string pathLoc      = Request.QueryString["pathLoc"];//客户端使用的是encodeURIComponent编码，
            string pathSvr      = Request.QueryString["pathSvr"];//客户端使用的是encodeURIComponent编码，
            pathLoc             = HttpUtility.UrlDecode(pathLoc);//utf-8解码
            pathSvr             = HttpUtility.UrlDecode(pathSvr);//utf-8编码
            string lengthLoc    = Request.QueryString["lengthLoc"];
            string lengthSvr    = Request.QueryString["lengthSvr"];
            string callback     = Request.QueryString["callback"];//应用于jsonp数据

            System.Diagnostics.Debug.WriteLine("uid:" + uid);
            System.Diagnostics.Debug.WriteLine("mac:" + mac);
            System.Diagnostics.Debug.WriteLine("pathLoc:" + pathLoc);
            System.Diagnostics.Debug.WriteLine("pathSvr:" + pathSvr);
            System.Diagnostics.Debug.WriteLine("lengthLoc:" + lengthLoc);
            System.Diagnostics.Debug.WriteLine("lengthSvr:" + lengthSvr);
            System.Diagnostics.Debug.WriteLine("callback:" + callback);

            if (string.IsNullOrEmpty(uid)
                || string.IsNullOrEmpty(mac)
                || string.IsNullOrEmpty(pathLoc)
                || string.IsNullOrEmpty(pathSvr)
                || string.IsNullOrEmpty(lengthLoc)
                || string.IsNullOrEmpty(lengthSvr))
            {
                Response.Write("参数为空");
                Response.End();
                return;
            }

            DnFileInf inf = new DnFileInf();
            inf.uid = int.Parse(uid);
            inf.mac = mac;
            inf.pathLoc = pathLoc;
            inf.pathSvr = pathSvr;
            inf.lengthLoc = lengthLoc;
            inf.lengthSvr = lengthSvr;
            DnFile db = new DnFile();
            db.Add(ref inf);

            string json = JsonConvert.SerializeObject(inf);
            //json = HttpUtility.UrlEncode(json);
            //json = json.Replace("+", "%20");
            json = callback + "(" + json + ")";//返回jsonp格式数据。
            Response.Write(json);
        }
    }
}
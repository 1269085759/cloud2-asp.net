using System;

namespace HttpUploader6.demoSql2005.db
{
    /// <summary>
    /// 以JSON格式返回文件夹信息。
    /// 客户端以JSONP格式提交
    /// </summary>
    /// <remarks>
    ///      [
    ///     [name:"soft"
    ///     ,pid:0                //父级ID
    ///     ,fid:0                //文件夹ID
    ///     ,length:"102032"      //数字化的文件夹大小
    ///     ,size:"10G"           //格式化的文件夹大小
    ///     ,pathLoc:"d:\\soft"   //文件夹在客户端的路径
    ///     ,pathSvr:"e:\\web"    //文件夹在服务端的路径
    ///     ,folders:[
    ///           {name:"img1",pidLoc:10,pidSvr:10,fidLoc:0,fidSvr:0,pathLoc:"D:\\Soft\\img1",pathSvr:"E:\\Web"}
    ///          ,{name:"img2",pidLoc:10,pidSvr:10,fidLoc:0,fidSvr:0,pathLoc:"D:\\Soft\\image2",pathSvr:"E:\\Web"}
    ///          ,{name:"img3",pidLoc:10,pidSvr:10,fidLoc:0,fidSvr:0,pathLoc:"D:\\Soft\\image2\\img3",pathSvr:"E:\\Web"}
    ///     ]
    ///     ,files:[
    ///           {name:"f1.exe",pidLoc:1,pidSvr:0,idLoc:0,idSvr:1,length:100,size:100KB,pathLoc:"",folder:"D:\\Soft\\"}
    ///          ,{name:"f2.exe",pidLoc:1,pidSvr:0,idLoc:0,idSvr:1,length:100,size:100KB,pathLoc:"",folder:"D:\\Soft\\c1"}
    ///          ,{name:"f3.exe",pidLoc:1,pidSvr:0,idLoc:0,idSvr:1,length:100,size:100KB,pathLoc:"",folder:"D:\\Soft\\network\\"}
    ///          ,{name:"f4.rar",pidLoc:1,pidSvr:0,idLoc:0,idSvr:1,length:100,size:100KB,pathLoc:"",folder:"D:\\Soft\\f3\\f31\\"}]]
    ///]
    /// </remarks>
    public partial class fd_json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string fid = Request.QueryString["fid"];
            string cbk = Request.QueryString["callback"];

            if (string.IsNullOrEmpty(fid) || string.IsNullOrEmpty(cbk))
            {
                Response.Write(cbk + "(0)");
                return;
            }

            //获取文件夹信息，和未完成的文件信息列表
            string json = DBFolder.GetFilesUnComplete(fid);

            Response.Write(cbk + "(" + json + ")");//必须返回jsonp格式数据
        }
    }
}
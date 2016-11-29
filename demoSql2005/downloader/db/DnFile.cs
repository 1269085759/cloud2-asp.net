using HttpUploader6.demoSql2005.db;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Web;

namespace HttpUploader6.demoSql2005.downloader.db
{
    public class DnFile
    {
        /// <summary>
        /// 从xdb_files中复制数据到down_files中
        /// </summary>
        /// <param name="fidOld">旧的文件夹ID</param>
        /// <param name="fidNew">新的文件夹ID</param>
        public static void Copy(int fidOld, int fidNew)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select fid");
            sb.Append(",f_pid");
        }

        /// <summary>
        /// 向down_files添加一个文件夹任务
        /// </summary>
        /// <param name="inf"></param>
        /// <returns></returns>
        public static int Add(ref DnFolderInf inf)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into down_files(");
            sb.Append("f_uid");
            sb.Append(",f_mac");
            sb.Append(",f_pathLoc");
            sb.Append(",f_fdID)");
            sb.Append(" values(");
            sb.Append("@f_uid");
            sb.Append(",@f_mac");
            sb.Append(",@f_pathLoc");
            sb.Append(",@f_fdID);");
            //
            sb.Append("select @@IDENTITY");

            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sb.ToString());
            db.AddInt(ref cmd, "@f_uid", inf.uid);
            db.AddString(ref cmd, "@f_mac", inf.mac, 50);
            db.AddString(ref cmd, "@f_pathLoc", inf.pathLoc, 255);
            db.AddInt(ref cmd, "@f_fdID", inf.idSvr);
            inf.idF = Convert.ToInt32(db.ExecuteScalar(ref cmd) );
            return inf.idF;
        }

        public int Add(ref DnFileInf inf)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("insert into down_files(");
            sql.Append("f_uid");
            sql.Append(",f_mac");
            sql.Append(",f_pathLoc");
            sql.Append(",f_pathSvr");
            sql.Append(",f_lengthLoc");
            sql.Append(",f_lengthSvr");
            sql.Append(") values(");
            sql.Append("@f_uid");
            sql.Append(",@f_mac");
            sql.Append(",@f_pathLoc");
            sql.Append(",@f_pathSvr");
            sql.Append(",@f_lengthLoc");
            sql.Append(",@f_lengthSvr");
            sql.Append(");");
            sql.Append("select @@IDENTITY");

            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql.ToString());
            db.AddInt(ref cmd, "@f_uid", inf.uid);
            db.AddString(ref cmd, "@f_mac", inf.mac, 50);
            db.AddString(ref cmd, "@f_pathLoc", inf.pathLoc, 255);
            db.AddString(ref cmd, "@f_pathSvr", inf.pathSvr, 255);
            db.AddString(ref cmd, "@f_lengthLoc", inf.lengthLoc, 19);
            db.AddString(ref cmd, "@f_lengthSvr", inf.lengthSvr, 19);
            object id = db.ExecuteScalar(ref cmd);
            inf.idSvr = Convert.ToInt32(id);
            //inf.idSvr = (int)db.ExecuteScalar(ref cmd);
            //cmd.Connection.Open();
            //cmd.ExecuteNonQuery();
            //cmd.Parameters.Clear();
            //cmd.CommandText = "select @@IDENTITY";
            //inf.idSvr = (int)cmd.ExecuteScalar();
            //cmd.Connection.Close();
            return inf.idSvr;
        }

        /// <summary>
        /// 将文件设为已完成
        /// </summary>
        /// <param name="fid"></param>
        public void Complete(int fid)
        {
            string sql = "update down_files set f_complete=1 where f_id=@f_id;";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddInt(ref cmd, "@f_id", fid);
            db.ExecuteNonQuery(ref cmd);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fid"></param>
        public void Delete(int fid, int uid, string mac)
        {
            string sql = "delete from down_files where f_id=@f_id and f_uid=@f_uid and f_mac=@f_mac;";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddInt(ref cmd, "@f_id", fid);
            db.AddInt(ref cmd, "@f_uid", uid);
            db.AddString(ref cmd, "@f_mac", mac, 50);
            db.ExecuteNonQuery(ref cmd);
        }

        public void UpdateLengthLoc(int fid, int uid, string mac, string lenLoc, string percent)
        {
            string sql = "update down_files set f_lengthLoc=@lenLoc,f_percent=@f_percent where f_id=@f_id and f_uid=@f_uid and f_mac=@f_mac;";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddString(ref cmd, "@lenLoc", lenLoc, 19);
            db.AddString(ref cmd, "@f_percent", percent, 6);
            db.AddInt(ref cmd, "@f_id", fid);
            db.AddInt(ref cmd, "@f_uid", uid);
            db.AddString(ref cmd, "@f_mac", mac, 50);
            db.ExecuteNonQuery(ref cmd);
        }

        /// <summary>
        /// 获取所有未下载完的文件和文件夹列表
        /// </summary>
        /// <returns></returns>
        public static string GetAll(int uid, string mac)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            sb.Append(" f_id");
            sb.Append(",f_mac");
            sb.Append(",f_pathLoc");
            sb.Append(",f_pathSvr");
            sb.Append(",f_lengthLoc");
            sb.Append(",f_percent");
            sb.Append(",fd_name");
            sb.Append(",fd_id");
            sb.Append(",fd_pathLoc");
            sb.Append(",fd_id_old");
            sb.Append(",fd_percent");
            sb.Append(" from down_files as f");
            sb.Append(" left join down_folders as fd");
            sb.Append(" on fd.fd_id = f.f_fdID");
            sb.Append(" where f_uid=@f_uid and f_mac=@f_mac and f_complete=0");
            //string sql = "select * from down_files where f_uid=@f_uid and f_mac=@f_mac and f_complete=0;";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sb.ToString());
            db.AddInt(ref cmd, "@f_uid", uid);
            db.AddString(ref cmd, "@f_mac", mac, 50);
            DbDataReader r = db.ExecuteReader(ref cmd);
            JArray files = new JArray();
            while (r.Read())
            {
                DnFileInf f = new DnFileInf();
                f.idSvr = r.GetInt32(0);
                f.mac = r.GetString(1);
                f.pathLoc = r.IsDBNull(2) ? string.Empty : r.GetString(2);
                f.pathSvr = r.IsDBNull(3) ? string.Empty : r.GetString(3);
                f.lengthLoc = r.IsDBNull(4) ? string.Empty : r.GetString(4);
                f.percent = r.IsDBNull(5) ? "0%" : r.GetString(5);
                f.name = Path.GetFileName(f.pathLoc);
                f.fdID = r.IsDBNull(7)? 0 : r.GetInt32(7);
                //文件夹
                if (0 != f.fdID)
                {
                    f.name = r.IsDBNull(6) ? string.Empty : r.GetString(6);
                    f.fdTask = true;
                    f.percent = r.IsDBNull(10) ? string.Empty : r.GetString(10);
                    f.pathLoc = r.IsDBNull(8) ? string.Empty : r.GetString(8);
                }
                files.Add((JObject)JToken.FromObject(f));
            }
            r.Close();

            //JArray arrFiles = new JArray();
            //foreach (DnFileInf f in files)
            //{
            //    arrFiles.Add((JObject)JToken.FromObject(f));
            //}
            return files.ToString();
        }

        static public void Clear()
        {
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand("delete from down_files;");
            db.ExecuteNonQuery(ref cmd);
        }
    }
}
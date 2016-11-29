using HttpUploader6.demoSql2005.db;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace HttpUploader6.demoSql2005.downloader.db
{
    public class DnFolder
    {
        /// <summary>
        /// 从xdb_folders中复制数据到down_folders中
        /// 从xdb_files中复制数据到down_files中
        /// </summary>
        /// <param name="fid">xdb_folders中的fd_id</param>
        public static int Add(ref DnFolderInf dfi)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into down_folders(fd_name");
            sb.Append(",fd_uid");
            sb.Append(",fd_mac");
            sb.Append(",fd_pathLoc");
            sb.Append(",fd_id_old)");
            //
            sb.Append(" values(@fd_name");
            sb.Append(",@fd_uid");
            sb.Append(",@fd_mac");
            sb.Append(",@fd_pathLoc");
            sb.Append(",@fd_id_old)");

            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sb.ToString());
            db.AddString(ref cmd, "@fd_name", dfi.name, 255);
            db.AddInt(ref cmd, "@fd_uid", dfi.uid);
            db.AddString(ref cmd, "@fd_mac", dfi.mac, 255);
            db.AddString(ref cmd, "@fd_pathLoc", dfi.pathLoc, 255);
            db.AddInt(ref cmd, "@fd_id_old", dfi.fdID);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            //获取新插入的ID
            cmd.Parameters.Clear();
            cmd.CommandText = "select @@IDENTITY;";
            object fd_id = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return Convert.ToInt32(fd_id);
        }

        /// <summary>
        /// 清空数据库
        /// </summary>
        public static void Clear()
        {
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand("delete from down_folders;");
            db.ExecuteNonQuery(ref cmd);
        }

        public static void Del(string idF, string idFD, string uid, string mac)
        {
            string sql = "delete from down_folders where fd_id=@fid and fd_mac=@mac and fd_uid=@uid;";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddInt(ref cmd, "@fid", int.Parse(idFD));
            db.AddString(ref cmd, "@mac", mac, 50);
            db.AddInt(ref cmd, "@uid", int.Parse(uid));
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            //删除down_files
            sql = "delete from down_files where f_id=@fid and f_mac=@mac and f_uid=@uid;";
            cmd.CommandText = sql;
            cmd.Parameters[0].Value = int.Parse(idF);
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public static void Update(string fid, string uid, string mac, string percent)
        {
            string sql = "update down_folders set fd_percent=@percent where fd_id=@fid and fd_uid=@uid and fd_mac=@mac";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddString(ref cmd, "@percent", percent, 7);
            db.AddInt(ref cmd, "@fid", int.Parse(fid));
            db.AddInt(ref cmd, "@uid", int.Parse(uid));
            db.AddString(ref cmd, "@mac", mac, 50);
            db.ExecuteNonQuery(ref cmd);
        }

        public static string GetFolderData(int fid, ref FolderInf root)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            sb.Append("xf.fd_name");
            sb.Append(",xf.fd_length");
            sb.Append(",xf.fd_size");
            sb.Append(",xf.fd_pid");
            sb.Append(",xf.fd_pathLoc");
            sb.Append(",xf.fd_pathSvr");
            sb.Append(",xf.fd_folders");
            sb.Append(",xf.fd_files");
            sb.Append(",xf.fd_filesComplete");
            sb.Append(" from down_folders as df");
            sb.Append(" left join xdb_files as xf");
            sb.Append(" on xf.fd_id = df.fd_id");
            sb.Append(" where df.fd_id=@fd_id and xf.fd_complete=1;");

            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sb.ToString());
            db.AddInInt32(cmd, "@fd_id", fid);
            DbDataReader r = db.ExecuteReader(cmd);
            //FolderInf root = new FolderInf();
            if (r.Read())
            {
                root.m_name = r.GetString(0);
                root.m_lenLoc = r.GetInt64(1);
                root.m_size = r.GetString(2);
                root.m_pidSvr = r.GetInt32(3);
                root.m_idSvr = fid;
                root.m_pathLoc = r.GetString(4);
                root.m_pathSvr = r.GetString(5);
                root.foldersCount = r.GetInt32(6);
                root.filesCount = r.GetInt32(7);
                root.filesComplete = r.GetInt32(8);
            }
            r.Close();

            //单独取已上传长度
            root.lenSvr = DBFolder.GetLenPosted(fid);

            //取文件信息
            JArray files = new JArray();
            List<string> ids = new List<string>();
            DBFile.GetCompletes(fid, ref files, ref ids);

            JObject obj = (JObject)JToken.FromObject(root);
            obj["files"] = files;
            obj["length"] = root.m_lenLoc;
            obj["ids"] = string.Join(",", ids.ToArray());
            return obj.ToString();
        }
    }
}
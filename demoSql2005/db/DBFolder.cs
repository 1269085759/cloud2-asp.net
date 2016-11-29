using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json.Linq;

namespace HttpUploader6.demoSql2005.db
{
    public class DBFolder
    {
        /// <summary>
        /// 向数据库添加一条记录
        /// </summary>
        /// <param name="inf"></param>
        /// <returns></returns>
        static public int Add(ref FolderInf inf)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into cloud2_folders(");
            sb.Append("fd_name");
            sb.Append(",fd_pid");
            sb.Append(",fd_uid");
            sb.Append(",fd_length");
            sb.Append(",fd_size");
            sb.Append(",fd_pathLoc");
            sb.Append(",fd_pathSvr");
            sb.Append(",fd_folders");
            sb.Append(",fd_files");
            sb.Append(",fd_pidRoot");
            sb.Append(",fd_pathRel");

            sb.Append(") values(");
            sb.Append("@fd_name");
            sb.Append(",@pid");
            sb.Append(",@uid");
            sb.Append(",@length");
            sb.Append(",@size");
            sb.Append(",@pathLoc");
            sb.Append(",@pathSvr");
            sb.Append(",@folders");
            sb.Append(",@files");
            sb.Append(",@pidRoot");
            sb.Append(",@pathRel");
            sb.Append(");");
            //
            sb.Append("SELECT @@IDENTITY");

            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sb.ToString());

            db.AddString(ref cmd, "@fd_name", inf.nameLoc, 50);
            db.AddInt(ref cmd, "@pid", inf.pidSvr);
            db.AddInt(ref cmd, "@uid", inf.uid);
            db.AddInt64(ref cmd, "@length", inf.lenLoc);
            db.AddString(ref cmd, "@size", inf.size, 50);
            db.AddString(ref cmd, "@pathLoc", inf.m_pathLoc, 255);
            db.AddString(ref cmd, "@pathSvr", inf.m_pathSvr, 255);
            db.AddInt(ref cmd, "@folders", inf.foldersCount);
            db.AddInt(ref cmd, "@files", inf.filesCount);
            db.AddInt(ref cmd, "@pidRoot", inf.pidRoot);//为下载控件提供支持
            db.AddString(ref cmd, "@pathRel", inf.pathRel,255);//为下载控件提供支持

            //获取新插入的ID
            object fid = db.ExecuteScalar(cmd);
            return Convert.ToInt32(fid);
        }

        /// <summary>
        /// 获取文件夹JSON数据
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static string GetFolderData(int fid, ref FolderInf root)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            sb.Append("fd_name");
            sb.Append(",fd_length");
            sb.Append(",fd_size");
            sb.Append(",fd_pid");
            sb.Append(",fd_pathLoc");
            sb.Append(",fd_pathSvr");
            sb.Append(",fd_folders");
            sb.Append(",fd_files");
            sb.Append(",fd_filesComplete");
            sb.Append(" from cloud2_folders");
            sb.Append(" where fd_id=@fd_id and fd_complete=1;");

            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sb.ToString());
            db.AddInt(ref cmd, "@fd_id", fid);
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
            obj["ids"] = string.Join(",", ids.ToArray());//
            return obj.ToString();
        }

        /// <summary>
        /// 将文件夹上传状态设为已完成
        /// </summary>
        /// <param name="fid">文件夹ID</param>
        /// <param name="uid">用户ID</param>
        static public void Complete(int fid, int uid)
        {
            string sql = "update cloud2_folders set fd_complete=1 where fd_id=@fd_id and fd_uid=@fd_uid;";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddInt(ref cmd, "@fd_id", fid);
            db.AddInt(ref cmd, "@fd_uid", uid);
            db.ExecuteNonQuery(cmd);
        }

        static public void Remove(int fid, int uid)
        {
            string sql = "update cloud2_folders set fd_delete=1 where fd_id=@ID and fd_uid=@fd_uid;";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddInt(ref cmd, "@ID", fid);
            db.AddInt(ref cmd, "@fd_uid", uid);
            db.ExecuteNonQuery(cmd);
        }

        static public void Clear()
        {
            string sql = "delete from cloud2_folders";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据文件夹ID获取文件夹信息和未上传完的文件列表，转为JSON格式。
        /// 说明：
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        static public string GetFilesUnComplete(int fid, ref FolderInf fd)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            sb.Append("fd_name");
            sb.Append(",fd_length");
            sb.Append(",fd_size");
            sb.Append(",fd_pid");
            sb.Append(",fd_pathLoc");
            sb.Append(",fd_pathSvr");
            sb.Append(",fd_folders");
            sb.Append(",fd_files");
            sb.Append(",fd_filesComplete");
            sb.Append(" from cloud2_folders where fd_id=@fd_id;");

            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sb.ToString());
            db.AddInt(ref cmd, "@fd_id", fid);
            DbDataReader r = db.ExecuteReader(cmd);
            //FolderInf root = new FolderInf();
            if (r.Read())
            {
                fd.m_name = r.GetString(0);
                fd.m_lenLoc = r.GetInt64(1);
                fd.m_size = r.GetString(2);
                fd.m_pidSvr = r.GetInt32(3);
                fd.m_idSvr = fid;
                fd.idFile = fid;//将文件夹与文件关联
                fd.m_pathLoc = r.GetString(4);
                fd.m_pathSvr = r.GetString(5);
                fd.foldersCount = r.GetInt32(6);
                fd.filesCount = r.GetInt32(7);
                fd.filesComplete = r.GetInt32(8);
            }
            r.Close();

            //单独取已上传长度
            //fd.lenPosted = DBFolder.GetLenPosted(fid).ToString();

            //取文件信息
            JArray files = new JArray();
            DBFile.GetUnCompletes(fid, ref files);

            JObject obj = (JObject)JToken.FromObject(fd);
            obj["files"] = files;
            return obj.ToString();
        }

        /// <summary>
        /// 根据文件夹ID获取文件夹信息和未上传完的文件列表，转为JSON格式。
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        static public string GetFilesUnComplete(string fid)
        {
            return GetFilesUnComplete(fid);
        }

        static public FolderInf GetInf(string fid)
        {
            FolderInf inf = new FolderInf();
            GetInf(ref inf, fid);
            return inf;
        }

        /// <summary>
        /// 根据文件夹ID填充文件夹信息
        /// </summary>
        /// <param name="inf"></param>
        /// <param name="fid"></param>
        static public bool GetInf(ref FolderInf inf, string fid)
        {
            bool ret = false;
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            sb.Append(" fd_name");
            sb.Append(",fd_length");
            sb.Append(",fd_size");
            sb.Append(",fd_pid");
            sb.Append(",fd_pathLoc");
            sb.Append(",fd_pathSvr");
            sb.Append(",fd_folders");
            sb.Append(",fd_files");
            sb.Append(",fd_filesComplete");
            sb.Append(" from cloud2_folders where fd_id=@fd_id;");

            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sb.ToString());
            db.AddInt(ref cmd, "@fd_id", Convert.ToInt32(fid));
            DbDataReader r = db.ExecuteReader(cmd);
            if (r.Read())
            {
                inf.m_name = r.GetString(0);
                inf.m_lenLoc = r.GetInt64(1);
                inf.m_size = r.GetString(2);
                inf.m_pidSvr = r.GetInt32(3);
                inf.m_idSvr = int.Parse(fid);
                inf.pathLoc = r.GetString(4);
                inf.pathSvr = r.GetString(5);
                inf.foldersCount = r.GetInt32(6);
                inf.filesCount = r.GetInt32(7);
                inf.filesComplete = r.GetInt32(8);
                ret = true;
            }
            r.Close();
            return ret;
        }

        /// <summary>
        /// (已弃用)获取文件夹已上传大小
        /// 计算所有文件已上传大小。
        /// </summary>
        /// <param name="fidRoot"></param>
        /// <returns></returns>
        static public long GetLenPosted(int fidRoot)
        {
            string sql = "select sum(tb.lenPosted) from (select distinct f_md5,CAST(f_lenSvr AS bigint) as lenPosted from cloud2_files where f_pidRoot=@f_pidRoot and f_md5 IS NOT NULL) as tb";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddInt(ref cmd, "@f_pidRoot", fidRoot);
            object len = db.ExecuteScalar(cmd);

            return DBNull.Value == len ? 0 : Convert.ToInt64(len);
        }

        /// <summary>
        /// 子文件上传完毕
        /// </summary>
        /// <param name="fd_idSvr"></param>
        static public void child_complete(int fd_idSvr)
        {
            string sql = "update cloud2_folders set fd_filesComplete=fd_filesComplete+1 where fd_id=@fd_id";
            DbHelper db = new DbHelper();
            DbCommand cmd = db.GetCommand(sql);
            db.AddInt(ref cmd, "@fd_id", fd_idSvr);
            db.ExecuteNonQuery(cmd);
        }
    }
}
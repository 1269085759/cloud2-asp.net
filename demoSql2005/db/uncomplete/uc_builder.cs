using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace HttpUploader6.demoSql2005.db.uncomplete
{
    /// <summary>
    /// 任务创建器
    /// </summary>
    public class uc_builder
    {
        public Dictionary<int, uc_folder> folders = new Dictionary<int, uc_folder>();//文件夹
        public List<xdb_files> files = new List<xdb_files>();//文件列表

        public uc_builder()
        {
        }

        /// <summary>
        /// 添加一个文件项
        /// </summary>
        public void add_file(ref DbDataReader r,int uid)
        {
            xdb_files f = new xdb_files();
            f.uid       = uid;
            f.idSvr     = r.GetInt32(0);
            f.f_fdTask  = r.GetBoolean(2);
            f.f_fdID    = r.GetInt32(3);
            f.nameLoc   = r.GetString(6);
            f.pathLoc   = r.GetString(7);
            f.md5       = r.GetString(8);
            f.lenLoc    = r.GetInt64(9);
            f.sizeLoc   = r.GetString(10);
            f.FilePos   = r.GetInt64(11);
            f.lenSvr    = r.GetInt64(12);
            f.perSvr    = r.GetString(13);
            f.complete  = r.GetBoolean(14);
            f.pathSvr   = r.GetString(15);//fix(2015-03-19):修复无法续传文件的问题。

            files.Add(f);
        }

        public void update_folder(ref DbDataReader r,int fd_id)
        {
			uc_folder fd;
            if (!this.folders.TryGetValue(fd_id, out fd)) fd = new uc_folder();
            this.folders.Remove(fd_id);

            FolderInf fdSvr = new FolderInf();
            fdSvr.filesComplete = r.GetInt32(24);
            fdSvr.filesCount = r.GetInt32(23);
            fdSvr.foldersCount = r.GetInt32(22);
            fdSvr.idFile = r.GetInt32(0);
            fdSvr.idSvr = r.GetInt32(3);
            fdSvr.lenLoc = r.GetInt64(9);
            fdSvr.lenSvr = r.GetInt64(12);
            fdSvr.pathLoc = r.GetString(20);
            fdSvr.pathSvr = r.GetString(21);
            fdSvr.size = r.GetString(18);
            fdSvr.nameLoc = r.GetString(16);
            fdSvr.perSvr = r.GetString(13);

            fd.m_fdSvr = fdSvr;
            this.folders.Add(fd_id, fd);
        }

        /// <summary>
        /// 添加一个文件夹项
        /// </summary>
        public void add_child(ref DbDataReader r,int pidRoot)
        {
            uc_file_child uf = new uc_file_child();
            uf.read(pidRoot, ref r);

            uc_folder fd;
            if (!this.folders.TryGetValue(pidRoot, out fd))
            {
                fd = new uc_folder();
                this.folders.Add(pidRoot, fd);
            }
            fd.m_files.Add(uf);
        }

        public string to_json()
        {
            JArray arrFiles = new JArray();
            foreach (xdb_files f in this.files)
            {
                //是文件夹任务=>取文件夹JSON
                if (f.f_fdTask)
                {

                    uc_folder fd;
                    if(this.folders.TryGetValue(f.f_fdID, out fd) )
                    {
                        f.perSvr = fd.m_fdSvr.perSvr;
                        f.fd_json = fd.getJson();
                    }
                }
                arrFiles.Add((JObject)JToken.FromObject(f));
            }
            if (arrFiles.Count > 0)
            {
                return JsonConvert.SerializeObject(arrFiles);
            }
            return null;
        }
    }
}
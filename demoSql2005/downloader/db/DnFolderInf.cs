using System;
using System.Collections.Generic;
using System.Web;

namespace HttpUploader6.demoSql2005.downloader.db
{
    public class DnFolderInf
    {
        public DnFolderInf()
        {
            this.m_fdID = this.m_uid = this.m_idSvr = 0;
            this.m_mac = this.m_name = this.m_pathLoc = string.Empty;
        }

        public int idSvr { get { return this.m_idSvr; } set { this.m_idSvr = value; } }
        public int uid { get { return m_uid; } set { this.m_uid = value; } }
        public string pathLoc { get { return this.m_pathLoc; } set { this.m_pathLoc = value; } }
        public string name { get { return this.m_name; } set { this.m_name = value; } }
        public string mac { get { return this.m_mac; } set { this.m_mac = value; } }
        public int fdID { get { return this.m_fdID; } set { this.m_fdID = value; } }
        public int idF { get { return this.m_idF; } set { this.m_idF = value; } }

        private int m_idSvr;//down_folders.fd_id
        private int m_uid;
        private int m_fdID;//文件夹ID，与xdb_folders.fd_id对应
        private int m_idF;//与down_files对应
        private string m_name;
        private string m_pathLoc;
        private string m_mac;
    }
}
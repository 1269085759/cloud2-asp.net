using Newtonsoft.Json;

namespace HttpUploader6.demoSql2005.downloader.db
{
    public class DnFileInf
    {
        public DnFileInf()
        {
            this.m_fdTask = false;
        }

        public int idSvr { get { return this.m_fid; } set { this.m_fid = value; } }
        public int uid { get { return this.m_uid; } set { this.m_uid = value; } }
        public string mac { get { return this.m_mac; } set { this.m_mac = value; } }
        public string pathLoc { get { return this.m_pathLoc; } set { this.m_pathLoc = value; } }
        public string pathSvr { get { return this.m_pathSvr; } set { this.m_pathSvr = value; } }
        public string lengthLoc { get { return this.m_lengthLoc; } set { this.m_lengthLoc = value; } }
        public string lengthSvr { get { return this.m_lengthSvr; } set { this.m_lengthSvr = value; } }
        public string percent { get { return this.m_percent; } set { this.m_percent = value; } }
        public string name { get { return this.m_name; } set { this.m_name = value; } }
        public bool fdTask { get { return this.m_fdTask; } set { this.m_fdTask = value; } }
        public int fdID { get { return this.m_fdID; } set { this.m_fdID = value; } }

        private string m_name;
        private bool m_fdTask;//是否是文件夹
        private int m_fdID;

        [JsonIgnore]
        public int m_fid;

        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonIgnore]
        public int m_uid;

        /// <summary>
        /// MAC地址
        /// </summary>
        [JsonIgnore]
        public string m_mac;

        /// <summary>
        /// 本地文件路径
        /// </summary>
        [JsonIgnore]
        public string m_pathLoc;

        /// <summary>
        /// 服务器文件路径
        /// </summary>
        [JsonIgnore]
        public string m_pathSvr;

        /// <summary>
        /// 本地文件长度
        /// </summary>
        [JsonIgnore]
        public string m_lengthLoc;

        /// <summary>
        /// 服务器文件长度
        /// </summary>
        [JsonIgnore]
        public string m_lengthSvr;

        /// <summary>
        /// 传输进度
        /// </summary>
        [JsonIgnore]
        public string m_percent;
    }
}
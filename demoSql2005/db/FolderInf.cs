using System.IO;
using Newtonsoft.Json;

namespace HttpUploader6.demoSql2005.db
{
    /// <summary>
    /// 文件夹信息
    /// </summary>
    public class FolderInf
    {
        public FolderInf()
        {
            this.m_name = this.m_size = this.m_pathLoc = this.m_pathSvr = string.Empty;
            this.m_lenSvr = this.m_lenLoc = 0;//fix:
            this.m_uid = this.m_pidLoc = this.m_pidSvr = this.m_idLoc = this.m_idSvr = 0;
        }

        public string nameLoc { get { return this.m_name; } set { this.m_name = value; } }
        public long lenLoc { get { return this.m_lenLoc; } set { this.m_lenLoc = value; } }
        public string size { get { return this.m_size; } set { this.m_size = value; } }
        public long lenSvr { get { return this.m_lenSvr; } set { this.m_lenSvr = value; } }
        public string perSvr { get { return this.m_perSvr; } set { this.m_perSvr = value; } }
        public int pidLoc { get { return this.m_pidLoc; } set { this.m_pidLoc = value; } }
        public int pidSvr { get { return this.m_pidSvr; } set { this.m_pidSvr = value; } }
        public int idLoc { get { return this.m_idLoc; } set { this.m_idLoc = value; } }
        public int idSvr { get { return this.m_idSvr; } set { this.m_idSvr = value; } }
        public int idFile{ get { return this.m_idFile; } set { this.m_idFile = value; } }
        public int uid { get { return this.m_uid; } set { this.m_uid = value; } }
        public int foldersCount { get { return this.m_folders; } set { this.m_folders = value; } }
        public int filesCount { get { return this.m_files; } set { this.m_files = value; } }
        public int filesComplete { get { return this.m_filesComplete; } set { this.m_filesComplete = value; } }

        /// <summary>
        /// 不进行URL编码，由ASPX页面进行统一编码
        /// </summary>
        public string pathLoc { get { return this.m_pathLoc; } set { this.m_pathLoc = value; } }

        /// <summary>
        /// 需要进行URL编解码，便于客户端不同编码的转码。
        /// </summary>
        public string pathSvr { get { return this.m_pathSvr; } set { this.m_pathSvr = value; } }
        public int pidRoot { get { return this.m_pidRoot; } set { this.m_pidRoot = value; } }
        public string pathRel { get { return this.m_pathRel; } set { this.m_pathRel = value; } }

        private int m_pidRoot=0;
        private string m_pathRel=string.Empty;

        [JsonIgnore]
        public string m_name;

        /// <summary>
        /// 数字化的长度，以字节为单位。示例：10252412
        /// </summary>
        [JsonIgnore]
        public long m_lenLoc;

        /// <summary>
        /// 已上传大小
        /// </summary>
        [JsonIgnore]
        public long m_lenSvr;
        public string m_perSvr;

        /// <summary>
        /// 格式化的长度，示例：10GB
        /// </summary>
        [JsonIgnore]
        public string m_size;

        /// <summary>
        /// 客户端父ID，提供给JS使用。
        /// </summary>
        [JsonIgnore]
        public int m_pidLoc;

        /// <summary>
        /// 服务端父ID，与数据库对应。
        /// </summary>
        [JsonIgnore]
        public int m_pidSvr;

        /// <summary>
        /// 客户端文件夹ID，提供给JS使用。
        /// </summary>
        [JsonIgnore]
        public int m_idLoc;

        /// <summary>
        /// 服务端文件夹ID,与数据库对应
        /// </summary>
        [JsonIgnore]
        public int m_idSvr;

        /// <summary>
        /// 与xdb_files.id关联
        /// </summary>
        [JsonIgnore]
        public int m_idFile;

        /// <summary>
        /// 子文件夹总数
        /// </summary>
        [JsonIgnore]
        public int m_folders;

        /// <summary>
        /// 子文件数
        /// </summary>
        [JsonIgnore]
        public int m_files;

        /// <summary>
        /// 已上传完的文件数
        /// </summary>
        [JsonIgnore]
        public int m_filesComplete;

        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonIgnore]
        public int m_uid;

        /// <summary>
        /// 文件夹在服务端路径。E:\\Web
        /// </summary>
        [JsonIgnore]
        public string m_pathSvr;

        /// <summary>
        /// 文件夹在客户端的路径。D:\\Soft\\Image
        /// </summary>
        [JsonIgnore]
        public string m_pathLoc;
    }
}
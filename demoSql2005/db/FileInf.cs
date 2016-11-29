using Newtonsoft.Json;

namespace HttpUploader6.demoSql2005.db
{
    /// <summary>
    /// 文件信息，与FolderInf配合使用。
    /// </summary>
    public class FileInf
    {
        public FileInf()
        {
            this.m_nameLoc = this.m_pathLoc = this.m_pathSvr = this.m_size = string.Empty;
            this.m_uid = this.m_pidLoc = this.m_pidSvr = this.m_idLoc = this.m_idSvr = 0;
            this.m_lenLoc = 0;
        }

        public string nameLoc { get { return this.m_nameLoc; } set { this.m_nameLoc = value; } }
        /// <summary>
        /// 由页面进行统一编解码
        /// </summary>
        public string pathLoc { get { return this.m_pathLoc; } set { this.m_pathLoc = value; } }
        /// <summary>
        /// 由页面进行统一编解码
        /// </summary>
        public string pathSvr { get { return this.m_pathSvr; } set { this.m_pathSvr = value; } }
        public string md5 { get { return this.m_md5; } set { this.m_md5 = value; } }
        public int pidLoc { get { return this.m_pidLoc; } set { this.m_pidLoc = value; } }
        public int pidSvr { get { return this.m_pidSvr; } set { this.m_pidSvr = value; } }
        public int pidRoot { get { return this.m_pidRoot; } set { this.m_pidRoot = value; } }
        public int idLoc { get { return this.m_idLoc; } set { this.m_idLoc = value; } }
        public int idSvr { get { return this.m_idSvr; } set { this.m_idSvr = value; } }
        public int uid { get { return this.m_uid; } set { this.m_uid = value; } }
        public long lenLoc { get { return this.m_lenLoc; } set { this.m_lenLoc = value; } }
        public string sizeLoc { get { return this.m_size; } set { this.m_size = value; } }
        public long lenSvr { get { return this.m_lenSvr; } set { this.m_lenSvr = value; } }
        public long postPos { get { return this.m_postPos; } set { this.m_postPos = value; } }
        public string perSvr { get { return this.m_perSvr; } set { this.m_perSvr = value; } }
        public string pathRel { get { return this.m_pathRel; } set { this.m_pathRel = value; } }
        public bool complete {
            get { return this.m_complete; }
            set { this.m_complete = value; }
        }
        public string nameSvr {
            get { return this.m_nameSvr; }
            set { this.m_nameSvr = value; }
        }

        /// <summary>
        /// 文件名称。示例：QQ2014.exe
        /// </summary>
        [JsonIgnore]
        public string m_nameLoc;
        private string m_nameSvr;

        /// <summary>
        /// 相对路径。root\\child\\folderName\\fileName.txt
        /// </summary>
        private string m_pathRel;

        /// <summary>
        /// 文件在客户端中的路径。示例：D:\\Soft\\QQ2013.exe
        /// </summary>
        [JsonIgnore]
        public string m_pathLoc;

        /// <summary>
        /// 文件在服务器上面的路径。示例：E:\\Web\\Upload\\QQ2013.exe
        /// </summary>
        [JsonIgnore]
        public string m_pathSvr;

        /// <summary>
        /// 客户端父ID(文件夹ID)
        /// </summary>
        [JsonIgnore]
        public int m_pidLoc;

        /// <summary>
        /// 服务端父ID(文件夹在数据库中的ID)
        /// </summary>
        [JsonIgnore]
        public int m_pidSvr;

        /// <summary>
        /// 根级文件夹ID，数据库ID，与xdb_folders.fd_id对应
        /// </summary>
        [JsonIgnore]
        public int m_pidRoot;

        /// <summary>
        /// 本地文件ID。
        /// </summary>
        [JsonIgnore]
        public int m_idLoc = 0;

        /// <summary>
        /// 文件在服务器中的ID。
        /// </summary>
        [JsonIgnore]
        public int m_idSvr = 0;

        /// <summary>
        /// 用户ID
        /// </summary>
        [JsonIgnore]
        public int m_uid = 0;

        /// <summary>
        /// 数字化的长度。以字节为单位，示例：1021021
        /// </summary>
        [JsonIgnore]
        public long m_lenLoc = 0;

        /// <summary>
        /// 格式化的长度。示例：10G
        /// </summary>
        [JsonIgnore]
        public string m_size = "0bytes";

        /// <summary>
        /// 文件上传位置。
        /// </summary>
        [JsonIgnore]
        public long m_postPos = 0;

        /// <summary>
        /// 上传百分比
        /// </summary>
        [JsonIgnore]
        public string m_perSvr = "0%";

        /// <summary>
        /// 已上传大小
        /// </summary>
        [JsonIgnore]
        public long m_lenSvr = 0;

        /// <summary>
        /// 文件MD5
        /// </summary>
        [JsonIgnore]
        public string m_md5 = string.Empty;
        private bool m_complete = false;
    }
}
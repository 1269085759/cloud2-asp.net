using System;
using Newtonsoft.Json;

namespace HttpUploader6.demoSql2005.db
{
    public class xdb_files
    {
        public int idSvr { get { return m_fid; } set { m_fid = value; } }
        public int pid { get { return m_pid; } set { m_pid = value; } }
        public int pidRoot { get { return m_pidRoot; } set { m_pidRoot = value; } }
        public bool f_fdTask { get { return m_f_fdTask; } set { m_f_fdTask = value; } }
        public int f_fdID { get { return m_f_fdID; } set { m_f_fdID = value; } }
        /// <summary>
        /// 是否是文件夹中的子文件
        /// </summary>
        public bool f_fdChild { get { return _f_fdChild; } set { _f_fdChild = value; } }
        public int uid { get { return m_uid; } set { m_uid = value; } }
        public string nameLoc { get { return m_nameLoc; } set { m_nameLoc = value; } }
        public string nameSvr { get { return m_nameSvr; } set { m_nameSvr = value; } }
        public string pathLoc { get { return m_pathLoc; } set { m_pathLoc = value; } }
        public string pathSvr { get { return m_pathSvr; } set { m_pathSvr = value; } }
        public string pathRel { get { return m_pathRel; } set { m_pathRel = value; } }
        public string md5 { get { return m_md5; } set { m_md5 = value; } }
        public long lenLoc { get { return m_length; } set { m_length = value; } }
        public string sizeLoc { get { return m_size; } set { m_size = value; } }
        public long FilePos { get { return m_postPos; } set { m_postPos = value; } }
        public long lenSvr { get { return m_postLength; } set { m_postLength = value; } }
        public string perSvr { get { return m_postPercent; } set { m_postPercent = value; } }
        public bool complete { get { return m_postComplete; } set { m_postComplete = value; } }
        public DateTime time { get { return m_postTime; } set { m_postTime = value; } }
        public bool deleted { get { return m_isDeleted; } set { m_isDeleted = value; } }
        public string fd_json { get { return m_fd_json; } set { m_fd_json = value; } }

        /// <summary>
        /// fid
        /// </summary>		
        [JsonIgnore]
        private int m_fid = 0;

        /// <summary>
        /// 文件夹ID
        /// </summary>
        [JsonIgnore]
        private int m_pid = 0;

        /// <summary>
        /// 根级文件夹ID
        /// </summary>
        [JsonIgnore]
        private int m_pidRoot = 0;

        /// <summary>
        /// 表示当前项是否是一个文件夹项。
        /// </summary>
        [JsonIgnore]
        private bool m_f_fdTask = false;

        /// <summary>
        /// 与xdb_folders.fd_id对应
        /// </summary>
        [JsonIgnore]
        private int m_f_fdID = 0;

        /// <summary>
        /// 表示文件是否是某个文件夹中的子文件
        /// </summary>
        [JsonIgnore]
        private bool _f_fdChild = false;

        /// <summary>
        /// 用户ID。与第三方系统整合使用。
        /// </summary>		
        [JsonIgnore]
        private int m_uid = 0;

        /// <summary>
        /// 文件在本地电脑中的名称。
        /// </summary>		
        [JsonIgnore]
        private string m_nameLoc = string.Empty;

        /// <summary>
        /// 文件在服务器中的名称。
        /// </summary>		
        [JsonIgnore]
        private string m_nameSvr = string.Empty;

        /// <summary>
        /// 文件在本地电脑中的完整路径。示例：D:\Soft\QQ2012.exe
        /// </summary>		
        [JsonIgnore]
        private string m_pathLoc = string.Empty;

        /// <summary>
        /// 文件在服务器中的完整路径。示例：F:\ftp\uer\md5.exe
        /// </summary>		
        [JsonIgnore]
        private string m_pathSvr = string.Empty;

        /// <summary>
        /// 文件在服务器中的相对路径。示例：/www/web/upload/md5.exe
        /// </summary>		
        [JsonIgnore]
        private string m_pathRel = string.Empty;

        /// <summary>
        /// 文件MD5
        /// </summary>		
        [JsonIgnore]
        private string m_md5 = string.Empty;

        /// <summary>
        /// 数字化的文件长度。以字节为单位，示例：120125
        /// 文件大小可能超过2G，所以使用long
        /// </summary>		
        [JsonIgnore]
        private long m_length = 0;

        /// <summary>
        /// 格式化的文件尺寸。示例：10.03MB
        /// </summary>		
        [JsonIgnore]
        private string m_size = string.Empty;

        /// <summary>
        /// 文件续传位置。
        /// 文件大小可能超过2G，所以使用long
        /// </summary>		
        [JsonIgnore]
        private long m_postPos = 0;

        /// <summary>
        /// 已上传大小。以字节为单位
        /// 文件大小可能超过2G，所以使用long
        /// </summary>		
        [JsonIgnore]
        private long m_postLength = 0;

        /// <summary>
        /// 已上传百分比。示例：10%
        /// </summary>		
        [JsonIgnore]
        private string m_postPercent = "0%";

        /// <summary>
        /// PostComplete
        /// </summary>		
        [JsonIgnore]
        private bool m_postComplete = false;

        /// <summary>
        /// PostedTime
        /// </summary>		
        [JsonIgnore]
        private DateTime m_postTime = DateTime.Now;

        /// <summary>
        /// IsDeleted
        /// </summary>		
        [JsonIgnore]
        private bool m_isDeleted = false;

        /// <summary>
        /// 文件夹JSON信息
        /// </summary>
        [JsonIgnore]
        private string m_fd_json = string.Empty;
    }
}

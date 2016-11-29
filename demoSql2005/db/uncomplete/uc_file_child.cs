using System.Data.Common;

namespace HttpUploader6.demoSql2005.db.uncomplete
{
    /// <summary>
    /// 子文件项，表示文件夹上传任务中的一个子文件
    /// </summary>
    public class uc_file_child : FileInf
    {
        public uc_file_child()
        {
        }

        public void read(int pidRoot,ref DbDataReader r)
        {
            this.idSvr = r.GetInt32(0);//
            this.m_nameLoc = (string)r["f_nameLoc"];
            this.m_pathLoc = (string)r["f_pathLoc"];
            this.m_lenLoc = (long)r["f_lenLoc"];
            this.m_size = (string)r["f_sizeLoc"];
            this.m_md5 = (string)r["f_md5"];
            this.m_pidRoot = pidRoot;
            this.pidSvr = (int)r["f_pid"];
            this.lenSvr = (long)r["f_lenSvr"];
            this.pathSvr = (string)r["f_pathSvr"];
        }
    }
}
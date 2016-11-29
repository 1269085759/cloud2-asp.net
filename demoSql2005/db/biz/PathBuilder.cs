using System;
using System.Collections.Generic;
using System.Web;

namespace HttpUploader6.demoSql2005.db.biz
{
    /// <summary>
    /// 路径生成器基类
    /// 提供文件或文件夹的存储路径
    /// </summary>
    public class PathBuilder
    {
        /// <summary>
        /// 根级存储路径,
        /// </summary>
        /// <returns></returns>
        public string getRoot()
        {
            return HttpContext.Current.Server.MapPath("/upload");
        }

        public virtual string genFolder(int uid, ref FolderInf fd)
        {
            return string.Empty;
        }

        public virtual string genFile(int uid, ref xdb_files f)
        {
            return string.Empty;
        }
        public virtual string genFile(int uid, string md5, string nameLoc)
        {
            return string.Empty;
        }
    }
}
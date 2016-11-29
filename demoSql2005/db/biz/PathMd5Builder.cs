using System;
using System.IO;

namespace HttpUploader6.demoSql2005.db.biz
{
    /// <summary>
    /// 所有文件以md5模式存储
    /// 所有文件夹中的文件以md5模式存储
    /// 所有文件均不重复
    /// </summary>
    public class PathMd5Builder : PathBuilder
    {
        /// <summary>
        /// 不创建文件夹，所有文件统一以日期格式存储
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="fd"></param>
        /// <returns></returns>
        public override string genFolder(int uid, ref FolderInf fd)
        {
            return string.Empty;
        }

        /// <summary>
        /// 所有文件均以md5模式存储
        /// 格式：
        ///     upload/年/月/日/md5.ext
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public override string genFile(int uid, ref xdb_files f)
        {
            DateTime timeCur = DateTime.Now;
            string path = Path.Combine(this.getRoot(), timeCur.ToString("yyyy"));
            path = Path.Combine(path, timeCur.ToString("MM"));
            path = Path.Combine(path, timeCur.ToString("dd"));
            string name = f.md5;
            name += Path.GetExtension(f.nameLoc);
            path = Path.Combine(path, name);

            return path;
        }
        public override string genFile(int uid, string md5,string nameLoc)
        {
            DateTime timeCur = DateTime.Now;
            string path = Path.Combine(this.getRoot(), timeCur.ToString("yyyy"));
            path = Path.Combine(path, timeCur.ToString("MM"));
            path = Path.Combine(path, timeCur.ToString("dd"));
            string name = md5;
            name += Path.GetExtension(nameLoc);
            path = Path.Combine(path, name);

            return path;
        }
    }
}
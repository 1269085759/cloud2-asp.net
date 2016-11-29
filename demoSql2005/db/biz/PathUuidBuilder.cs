using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace HttpUploader6.demoSql2005.db.biz
{
    /// <summary>
    /// 根据UUID生成存储路径
    /// 所有文件按原始文件名称存储
    /// 所有文件夹中的文件按原始文件名称存储
    /// 文件存在重复
    /// </summary>
    public class PathUuidBuilder : PathBuilder
    {
        /// <summary>
        /// 构建存储路径，保留客户端文件名称
        /// 格式：
        ///     upload/uid/folders/uuid/folder_name
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="fd"></param>
        /// <returns></returns>
        public override string genFolder(int uid, ref FolderInf fd)
        {
            var uuid = Guid.NewGuid().ToString("N"); // e0a953c3ee6040eaa9fae2b667060e09   
            DateTime timeCur = DateTime.Now;
            string path = Path.Combine(this.getRoot(), timeCur.ToString("yyyy"));
            path = Path.Combine(path, timeCur.ToString("MM"));
            path = Path.Combine(path, timeCur.ToString("dd"));
            path = Path.Combine(path, uuid);
            path = Path.Combine(path, fd.nameLoc);

            return path;
        }

        /// <summary>
        /// 保留原始文件名称
        /// 文件存在重复
        /// 格式：
        ///     upload/uid/年/月/日/uuid/file_name
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public override string genFile(int uid, ref xdb_files f)
        {
            var uuid = Guid.NewGuid().ToString("N"); // e0a953c3ee6040eaa9fae2b667060e09   
            DateTime timeCur = DateTime.Now;
            string path = Path.Combine(this.getRoot(), timeCur.ToString("yyyy"));
            path = Path.Combine(path, timeCur.ToString("MM"));
            path = Path.Combine(path, timeCur.ToString("dd"));
            path = Path.Combine(path, uuid);
            path = Path.Combine(path, f.nameLoc);

            return path;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace HttpUploader6.demoSql2005.db.biz
{
    public class PathCloudBuilder : PathBuilder
    {
        /// <summary>
        /// 云资源访问链接前缀
        /// </summary>
        public string m_url = "http://ncmem.oss-cn-shenzhen.aliyuncs.com/";


        /// <summary>
        /// 所有文件均以md5模式存储
        /// 格式：
        ///     /md5.ext
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public override string genFile(int uid, ref xdb_files f)
        {
            return this.genFile(uid, f.md5, f.nameLoc);
        }
        public override string genFile(int uid, string md5, string nameLoc)
        {
            string name = md5;
            string ext = Path.GetExtension(nameLoc);
            if (!this.m_url.EndsWith("/")) this.m_url += "/";
            return this.m_url + name + ext;
        }
    }
}
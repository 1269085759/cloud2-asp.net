using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace HttpUploader6.demoSql2005.db.uncomplete
{
    /// <summary>
    /// 未完成的文件夹信息
    /// ，自动从字典中拼接JSON
    /// </summary>
    public class uc_folder
    {
        public FolderInf m_fdSvr;

        public List<uc_file_child> m_files = new List<uc_file_child>();//子文件列表

        public uc_folder()
        {
        }

        public string getJson()
        {
            JObject obj = (JObject)JToken.FromObject(this.m_fdSvr);
            JArray jar = new JArray();
            foreach(var f in this.m_files)
            {
                jar.Add(JToken.FromObject(f));
            }
            //this.m_dicFolder.TryGetValue(this.fd_id, out files);
            obj["files"] = jar;
            return obj.ToString();
        }
    }
}
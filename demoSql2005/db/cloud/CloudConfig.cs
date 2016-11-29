using System;
using System.Collections.Generic;
using System.Web;

namespace HttpUploader6.demoSql2005.db.cloud
{
    public class CloudConfig
    {
        public string name {
            get { return this.m_name; }
            set { this.m_name = value; }
        }
        public string bucket {
            get { return this.m_bucket; }
            set { this.m_bucket = value; }
        }
        public string fileUrl { get { return this.m_fileUrl; }
            set { this.m_fileUrl = value; } }//文件访问地址
        public string ak
        {
            get { return this.m_ak; }
            set { this.m_ak = value; }
        }
        public string url {
            get { return this.m_url; }
            set { this.m_url = value; }
        }//上传地址
        public string folder {
            get { return this.m_folder; }
            set { this.m_folder = value; }
        }//远程目录

        private string m_name="";
        private string m_bucket = "";
        private string m_fileUrl = "http://www.ncmem.com/";
        private string m_ak = "";
        private string m_url = "";
        private string m_folder = "";
    }
}
using System.Web;
using System.IO;
using System.Threading;

namespace HttpUploader6.demoSql2005.db
{
    /// <summary>
    /// 文件续传类
    /// </summary>
    public class FileResumer
	{
		public long m_lenLoc;		//文件总大小。
		long m_RangePos;		//文件块起始位置
		public long RangePos
		{
			set { this.m_RangePos = value; }
		}

		//文件读写锁，防止多个用户同时上传相同文件时，出现创建文件的错误
		static ReaderWriterLock m_writeLock = new ReaderWriterLock();

		public FileResumer()
		{
		}

		/// <summary>
		/// 根据文件大小创建文件。
		/// 注意：多个用户同时上传相同文件时，可能会同时创建相同文件。
		/// </summary>
		public void CreateFile(string filePath)
		{
			//文件不存在则创建
			if (!File.Exists(filePath))
			{
				//创建文件
				//这里存在多个线程同时创建文件的问题。

                //自动创建目录
                if(!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

				FileStream fs = File.Create(filePath);
				BinaryWriter w = new BinaryWriter(fs);
                //fix(2015-03-16):不再按实际大小创建文件，减少上传大文件时用户等待的时间。
                w.Write((byte)0);
                //for (long i = 0; i < this.m_FileSize; ++i)
                //{
                //    w.Write((byte)0);
                //}
				w.Close();
				fs.Close();
			}
		}

		/// <summary>
		/// 续传文件
		/// </summary>
		/// <param name="fileRange">文件块</param>
		/// <param name="fileRemote">远程文件完整路径。d:\www\web\upload\201204\10\md5.exe</param>
		public void Resumer(ref HttpPostedFile fileRange,string fileRemote)
		{
			//存在多个用户同时创建相同文件的问题。
			m_writeLock.AcquireWriterLock(1000);
			this.CreateFile(fileRemote);
			m_writeLock.ReleaseWriterLock();

			//上传的文件大小不为空
			if (fileRange.InputStream.Length > 0)
			{
				//文件已存在，写入数据
				FileStream fs = new FileStream(fileRemote, FileMode.Open, FileAccess.Write, FileShare.Write);
				fs.Seek(this.m_RangePos, SeekOrigin.Begin);
				byte[] ByteArray = new byte[fileRange.InputStream.Length];
				fileRange.InputStream.Read(ByteArray, 0, (int)fileRange.InputStream.Length);
				fs.Write(ByteArray, 0, (int)fileRange.InputStream.Length);
				fs.Flush();
				fs.Close();
			}

		}
	}
}

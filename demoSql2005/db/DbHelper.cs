using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace HttpUploader6.demoSql2005.db
{
    /// <summary>
    /// 参考：http://www.cnblogs.com/JemBai/archive/2008/09/02/1281864.html
    /// 
    /// 直接SQL语句
    /// DbHelper db = new DbHelper();
    /// DbCommand cmd = db.GetSqlStringCommond("insert t1 (id)values('haha')");
    /// db.ExecuteNonQuery(cmd);
    /// 
    /// 执行存储过程
    /// DbHelper db = new DbHelper();
    ///	DbCommand cmd = db.GetStoredProcCommond("t1_insert");
    ///	db.AddInParameter(cmd, "@id", DbType.String, "heihei");
    ///	db.ExecuteNonQuery(cmd);
    ///	
    /// 返回DataSet
    /// DbHelper db = new DbHelper();
    ///	DbCommand cmd = db.GetSqlStringCommond("select * from t1");
    ///	DataSet ds = db.ExecuteDataSet(cmd);
    ///	
    /// 返回DataTable
    /// DbHelper db = new DbHelper();
    ///	DbCommand cmd = db.GetSqlStringCommond("t1_findall");
    ///	DataTable dt = db.ExecuteDataTable(cmd);
    ///	
    /// 输入参数/输出参数/返回值的使用
    ///	DbHelper db = new DbHelper();
    ///	DbCommand cmd = db.GetStoredProcCommond("t2_insert");
    ///	db.AddInParameter(cmd, "@timeticks", DbType.Int64, DateTime.Now.Ticks);
    ///	db.AddOutParameter(cmd, "@outString", DbType.String, 20);
    ///	db.AddReturnParameter(cmd, "@returnValue", DbType.Int32);

    ///	db.ExecuteNonQuery(cmd);

    ///	string s = db.GetParameter(cmd, "@outString").Value as string;//out parameter
    ///	int r = Convert.ToInt32(db.GetParameter(cmd, "@returnValue").Value);//return value
    ///	
    /// DataReader的使用
    /// DbHelper db = new DbHelper();
    ///	DbCommand cmd = db.GetStoredProcCommond("t2_insert");
    ///	db.AddInParameter(cmd, "@timeticks", DbType.Int64, DateTime.Now.Ticks);
    ///	db.AddOutParameter(cmd, "@outString", DbType.String, 20);
    ///	db.AddReturnParameter(cmd, "@returnValue", DbType.Int32);

    ///	using (DbDataReader reader = db.ExecuteReader(cmd))
    ///	{
    ///    dt.Load(reader);
    ///	}        
    ///	string s = db.GetParameter(cmd, "@outString").Value as string;//out parameter
    ///	int r = Convert.ToInt32(db.GetParameter(cmd, "@returnValue").Value);//return value
    /// </summary>
    public class DbHelper
    {
        private static string m_dbServer = @"(local)\SQL2005";	//数据库地址
        private static string m_dbUser = "sa";			    //数据库帐号
        private static string m_dbPass = "123456";			//数据库密码
        private static string m_dbName = "cloud2";	//数据库名称
        private static string dbProviderName = "System.Data.SqlClient";
        private DbConnection connection;

        /// <summary>
        /// 获取数据库连接字符串
        /// 示例："Data Source=myServerAddress;Initial Catalog=myDataBase;User Id=myUsername;Password=myPassword;
        /// </summary>
        /// <returns></returns>
        public static string GetConStr()
        {
            string conStr = "Data Source=%server%;Initial Catalog=%database%;User Id=%uid%;Password=%pwd%;";
            conStr = conStr.Replace("%server%", m_dbServer);
            conStr = conStr.Replace("%database%", m_dbName);
            conStr = conStr.Replace("%uid%", m_dbUser);
            conStr = conStr.Replace("%pwd%", m_dbPass);

            return conStr;
        }

        public DbHelper()
        {
            this.connection = CreateConnection(GetConStr());
        }

        public DbHelper(string connectionString)
        {
            this.connection = CreateConnection(GetConStr());
        }

        public static DbConnection CreateConnection()
        {
            //DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbConnection con = SqlClientFactory.Instance.CreateConnection();
            con.ConnectionString = GetConStr();

            return con;
        }

        public static DbConnection CreateConnection(string connectionString)
        {
            //DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbConnection con = SqlClientFactory.Instance.CreateConnection();
            con.ConnectionString = GetConStr();
            return con;
        }

        public DbCommand GetCommandStored(string storedProcedure)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = storedProcedure;
            dbCommand.CommandType = CommandType.StoredProcedure;
            return dbCommand;
        }

        public DbCommand GetCommand(string sqlQuery)
        {
            DbCommand dbCommand = connection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.CommandType = CommandType.Text;
            return dbCommand;
        }

        #region 增加参数
        public void AddParameterCollection(DbCommand cmd, DbParameterCollection dbParameterCollection)
        {
            foreach (DbParameter dbParameter in dbParameterCollection)
            {
                cmd.Parameters.Add(dbParameter);
            }
        }

        public void AddOutParameter(DbCommand cmd, string parameterName, DbType dbType, int size)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = size;
            dbParameter.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddInParameter(DbCommand cmd, string parameterName, DbType dbType, int paramLen, object value)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Size = paramLen;
            dbParameter.Value = value;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddString(ref DbCommand cmd, string pn, string pv, int plen, ParameterDirection pd = ParameterDirection.Input)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = DbType.String;
            dbParameter.ParameterName = pn;
            dbParameter.Size = plen;
            dbParameter.Value = pv;
            dbParameter.Direction = pd;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddInt(ref DbCommand cmd, string pn, int pv, ParameterDirection pd = ParameterDirection.Input)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = DbType.Int32;
            dbParameter.ParameterName = pn;
            dbParameter.Value = pv;
            dbParameter.Direction = pd;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddInt64(ref DbCommand cmd, string pn, long pv, ParameterDirection pd = ParameterDirection.Input)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = DbType.Int64;
            dbParameter.ParameterName = pn;
            dbParameter.Value = pv;
            dbParameter.Direction = pd;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddUInt32(ref DbCommand cmd, string pn, int pv, ParameterDirection pd = ParameterDirection.Input)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = DbType.UInt32;
            dbParameter.ParameterName = pn;
            dbParameter.Value = pv;
            dbParameter.Direction = pd;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddDate(ref DbCommand cmd, string pn, DateTime pv, ParameterDirection pd = ParameterDirection.Input)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = DbType.Date;
            dbParameter.ParameterName = pn;
            dbParameter.Value = pv;
            dbParameter.Direction = pd;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddBool(ref DbCommand cmd, string pn, bool pv, ParameterDirection pd = ParameterDirection.Input)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = DbType.Boolean;
            dbParameter.ParameterName = pn;
            dbParameter.Value = pv;
            dbParameter.Direction = pd;
            cmd.Parameters.Add(dbParameter);
        }

        /// <summary>
        /// 添加OleDb(Access)备注字段
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="pname"></param>
        /// <param name="pv"></param>
        public void AddMemo(DbCommand cmd, string pn, string pv, ParameterDirection pd = ParameterDirection.Input)
        {
            DbParameter param = cmd.CreateParameter();
            param.DbType = DbType.AnsiString;
            param.ParameterName = pn;
            param.Size = 65535;//备注字段固定长度
            param.Value = pv;
            param.Direction = pd;
            cmd.Parameters.Add(param);
        }

        public void AddInInt32(DbCommand cmd, string pname, int pv)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = DbType.Int32;
            dbParameter.ParameterName = pname;
            dbParameter.Value = pv;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }

        public void AddInBool(DbCommand cmd, string pname, bool pv)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = DbType.Boolean;
            dbParameter.ParameterName = pname;
            dbParameter.Value = pv;
            dbParameter.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(dbParameter);
        }

        /// <summary>
        /// 添加OleDb(Access)备注字段
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="pname"></param>
        /// <param name="pv"></param>
        public void AddInMemo(DbCommand cmd, string pname, string pv)
        {
            DbParameter param = cmd.CreateParameter();
            param.DbType = DbType.AnsiString;
            param.ParameterName = pname;
            param.Size = 65535;//备注字段固定长度
            param.Value = pv;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);
        }

        public void AddReturnParameter(DbCommand cmd, string parameterName, DbType dbType)
        {
            DbParameter dbParameter = cmd.CreateParameter();
            dbParameter.DbType = dbType;
            dbParameter.ParameterName = parameterName;
            dbParameter.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(dbParameter);
        }

        public DbParameter GetParameter(DbCommand cmd, string parameterName)
        {
            return cmd.Parameters[parameterName];
        }
        #endregion

        #region 执行
        public DataSet ExecuteDataSet(DbCommand cmd)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd)
        {
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DbDataReader ExecuteReader(DbCommand cmd)
        {
            cmd.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public DbDataReader ExecuteReader(ref DbCommand cmd)
        {
            cmd.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }

        public int ExecuteNonQuery(DbCommand cmd)
        {
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
        }

        public int ExecuteNonQuery(ref DbCommand cmd)
        {
            cmd.Connection.Open();
            int ret = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return ret;
        }

        public object ExecuteScalar(DbCommand cmd)
        {
            cmd.Connection.Open();
            object ret = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return ret;
        }

        public object ExecuteScalar(ref DbCommand cmd)
        {
            cmd.Connection.Open();
            object ret = cmd.ExecuteScalar();
            cmd.Connection.Close();
            return ret;
        }

        #endregion

        #region 执行事务
        public DataSet ExecuteDataSet(DbCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataSet ds = new DataSet();
            dbDataAdapter.Fill(ds);
            return ds;
        }

        public DataTable ExecuteDataTable(DbCommand cmd, Trans t)
        {
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbProviderFactory dbfactory = DbProviderFactories.GetFactory(DbHelper.dbProviderName);
            DbDataAdapter dbDataAdapter = dbfactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = cmd;
            DataTable dataTable = new DataTable();
            dbDataAdapter.Fill(dataTable); return dataTable;
        }
        public DbDataReader ExecuteReader(DbCommand cmd, Trans t)
        {

            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            DbDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            return reader;
        }

        public int ExecuteNonQuery(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            int ret = cmd.ExecuteNonQuery();
            return ret;
        }
        public object ExecuteScalar(DbCommand cmd, Trans t)
        {
            cmd.Connection.Close();
            cmd.Connection = t.DbConnection;
            cmd.Transaction = t.DbTrans;
            object ret = cmd.ExecuteScalar();
            return ret;
        }
        #endregion
    }

    public class Trans : IDisposable
    {
        private DbConnection conn;
        private DbTransaction dbTrans;
        public DbConnection DbConnection
        {
            get { return this.conn; }
        }
        public DbTransaction DbTrans
        {
            get { return this.dbTrans; }
        }
        public Trans()
        {
            conn = DbHelper.CreateConnection();
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public Trans(string connectionString)
        {
            conn = DbHelper.CreateConnection(connectionString);
            conn.Open();
            dbTrans = conn.BeginTransaction();
        }
        public void Commit()
        {
            dbTrans.Commit();
            this.Colse();
        }
        public void RollBack()
        { dbTrans.Rollback(); this.Colse(); }
        public void Dispose()
        { this.Colse(); }
        public void Colse()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
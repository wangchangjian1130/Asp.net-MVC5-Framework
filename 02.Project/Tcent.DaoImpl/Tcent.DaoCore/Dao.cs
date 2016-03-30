using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Tcent.Domain;

namespace Tcent.DaoCore
{
    /// <summary>
    /// 数据库访问类
    /// </summary>
    /// { Created At Time:[ 2016/3/21 17:15 ], By User:wcj21259, On Machine:WCJ }
    public class Dao
    {
        /// <summary>
        /// 数据库访问实例
        /// </summary>
        /// { Created At Time:[ 2016/3/21 17:15 ], By User:wcj21259, On Machine:WCJ }
        private static volatile Dao instance;

        /// <summary>
        /// 锁对象
        /// </summary>
        /// { Created At Time:[ 2016/3/21 17:16 ], By User:wcj21259, On Machine:WCJ }
        private static object lockHelper = new object();

        /// <summary>
        /// 枚举数据库类型
        /// </summary>
        public enum DBProviderType
        {
            SqlServer,
            OleDB,
            Odbc,
            Oracle,
            MySql
        }

        /// <summary>
        /// 数据库默认类型
        /// </summary>
        /// { Created At Time:[ 2016/3/21 19:26 ], By User:wcj21259, On Machine:WCJ }
        private static DBProviderType defaultcontype;

        /// <summary>
        /// 获取数据库默认类型
        /// </summary>
        /// <value>
        /// The type of the DAO con.
        /// </value>
        /// { Created At Time:[ 2015/11/23 10:01], By User:Jake Wang, On Machine:APP-DEV-JAKE}
        private static DBProviderType DaoConType
        {
            get
            {
                return defaultcontype;
            }
        }

        /// <summary>
        /// 懒汉单例
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        /// { Created At Time:[ 2015/11/23 10:01], By User:Jake Wang, On Machine:APP-DEV-JAKE}
        public static Dao Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new Dao();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 构造函数 Prevents a default instance of the <see cref="Dao"/> class from being created.
        /// </summary>
        /// { Created At Time:[ 2015/11/23 10:01], By User:Jake Wang, On Machine:APP-DEV-JAKE}
        private Dao()
        {
            Init();
        }

        /// <summary>
        /// 连接池Dictionary
        /// </summary>
        /// { Created At Time:[ 2016/3/21 17:18 ], By User:wcj21259, On Machine:WCJ }
        private Dictionary<string, IDbConnection> connPool = new Dictionary<string, IDbConnection>();

        /// <summary>
        /// 初始化.
        /// </summary>
        /// { Created At Time:[ 2016/3/21 17:18 ], By User:wcj21259, On Machine:WCJ }
        /// <exception cref="System.NotImplementedException"></exception>
        private void Init()
        {
            // 移除所有键值
            connPool.Clear();
            // 数据库路径
            var path = HttpContext.Current.Server.MapPath(@"~/Configs/database.config");
            try
            {
                var ds = new DataSet();
                if (System.IO.File.Exists(path))
                {
                    ds.ReadXml(path);
                }

                // 获取连接字符串集合
                var dbKeyList = ds.Tables["Database"].AsEnumerable().Select(dataRow => new { DBKey = dataRow.Field<string>("name") }).ToList();
                var connStrList = ds.Tables["server"].AsEnumerable().Select(dataRow => new { ConnectionString = dataRow.Field<string>("connectionString"), Enabled = dataRow.Field<string>("enabled") }).Distinct().ToList();

                // 添加数据库字符串连接到连接池
                for (int i = 0; i < connStrList.Count(); i++)
                {
                    connPool.Add(dbKeyList[i].DBKey, GetConnection(connStrList[i], defaultcontype));
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 从数据库连接池中获取.
        /// </summary>
        /// <param name="connstr">The connstr.</param>
        /// <returns></returns>
        /// { Created At Time:[ 2016/3/21 17:23 ], By User:wcj21259, On Machine:WCJ }
        public IDbConnection GetConnFromPool(string dbName = "", DBProviderType _defaultcontype = DBProviderType.SqlServer)
        {
            defaultcontype = _defaultcontype;

            IDbConnection c = null;
            if (!string.IsNullOrWhiteSpace(dbName))
            {
                c = connPool[dbName];
            }
            else
            {
                c = connPool.FirstOrDefault().Value;
            }

            if (c != null)
            {
                var conn = (IDbConnection)Activator.CreateInstance(c.GetType(), new object[] { c.ConnectionString });
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                return conn;
            }

            return null;
        }

        /// <summary>
        /// 获取对应的数据库连接对象.
        /// </summary>
        /// <param name="connStr">连接字符串.</param>
        /// <param name="providerType">数据库类型.</param>
        /// <returns>数据库连接对象</returns>
        /// { Created At Time:[ 2015/11/23 10:02], By User:Jake Wang, On Machine:APP-DEV-JAKE}
        public static IDbConnection GetConnection(dynamic connStr, DBProviderType providerType)
        {
            IDbConnection iDbConnection;
            string connectionstring = connStr.ConnectionString;
            if (Convert.ToBoolean(connStr.Enabled))
            {
                switch (providerType)
                {
                    case DBProviderType.OleDB:
                        iDbConnection = new System.Data.OleDb.OleDbConnection(connectionstring);
                        break;
                    case DBProviderType.Odbc:
                        iDbConnection = new System.Data.Odbc.OdbcConnection(connectionstring);
                        break;
                    //case DBProviderType.Oracle:
                    //    iDbConnection = new OracleConnection(connStr);
                    //    break;
                    //case DBProviderType.MySql:
                    //    iDbConnection = new MySql.Data.MySqlClient.MySqlConnection(connStr);
                    //    break;
                    case DBProviderType.SqlServer:
                    default:
                        iDbConnection = new System.Data.SqlClient.SqlConnection(connectionstring);
                        break;
                }
            }
            else
            {
                return null;
            }
            return iDbConnection;
        }
    }

    /// <summary>
    /// 查询实体基类
    /// </summary>
    /// { Created At Time:[ 2016/3/23 17:55 ], By User:wcj21259, On Machine:WCJ }
    public class Share
    {
        /// <summary>
        /// 每页记录条数.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        /// { Created At Time:[ 2016/3/23 17:56 ], By User:wcj21259, On Machine:WCJ }
        public static int PageSize
        {
            get
            {
                int pageSize = 0;
                int.TryParse(ConfigurationManager.AppSettings["PageSize"].ToString(), out pageSize);
                return pageSize;
            }
        }

        /// <summary>
        /// 页索引.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        /// { Created At Time:[ 2016/3/23 17:56 ], By User:wcj21259, On Machine:WCJ }
        public static int PageIndex
        {
            get
            {
                int pageIndex = 1;
                int.TryParse(ConfigurationManager.AppSettings["PageIndex"].ToString(), out pageIndex);
                return pageIndex;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">class type</typeparam>
        /// <param name="model">分页实体类</param>
        /// <param name="DbName">数据库</param>
        /// <returns></returns>
        public static BasePageList<T> ExecutePaging<T>(PagingModel model, string DbName = "") where T : class, new()
        {
            BasePageList<T> baseList = new BasePageList<T>();
            try
            {
                using (var conn = Dao.Instance.GetConnFromPool(DbName))
                {
                    // 参数化查询条件
                    var sqlParams = new DynamicParameters(model.SqlParams);

                    // 记录总数Sql
                    int total = conn.Query<int>(string.Format(@"select count(1) from ({0}) total", model.Sql), sqlParams).FirstOrDefault();

                    model.PageSize = model.PageSize.HasValue ? model.PageSize.Value : PageSize;
                    model.PageIndex = model.PageIndex.HasValue ? model.PageIndex : PageIndex;

                    // 分页查询Sql语句
                    string sql = @"SELECT TOP " + model.PageSize + " * FROM (SELECT *,ROW_NUMBER() OVER(ORDER BY " + model.OrderField + ") rn FROM (" +
                                   model.Sql + ") a ) query WHERE rn > " + (model.PageIndex - 1) * model.PageSize + " ORDER BY rn ";

                    // 执行结果
                    baseList.Models = conn.Query<T>(sql, sqlParams);
                    // 总记录数
                    baseList.TotalCount = total;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return baseList;
        }
    }

    /// <summary>
    /// 分页实体类
    /// </summary>
    public class PagingModel
    {
        /// <summary>
        /// 表名.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        /// { Created At Time:[ 2016/3/23 18:08 ], By User:wcj21259, On Machine:WCJ }
        public string Sql { get; set; }

        /// <summary>
        /// 查询字段.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        /// { Created At Time:[ 2016/3/23 18:08 ], By User:wcj21259, On Machine:WCJ }
        public string Fields { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        /// <value>
        /// The order field.
        /// </value>
        /// { Created At Time:[ 2016/3/23 18:08 ], By User:wcj21259, On Machine:WCJ }
        public string OrderField { get; set; }

        /// <summary>
        /// 查询条件参数
        /// </summary>
        public dynamic SqlParams { get; set; }

        /// <summary>
        /// 每页记录条数
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// 页索引
        /// </summary>
        public int? PageIndex { get; set; }

        // 构造函数
        public PagingModel(string _sql = "", string _fields = "", string _orderField = "",
            dynamic _sqlParams = null, int? _pageSize = null, int? _pageIndex = null)
        {
            Sql = _sql;
            Fields = _fields;
            OrderField = _orderField;
            SqlParams = _sqlParams;
            PageSize = _pageSize;
            PageIndex = _pageIndex;
        }
    }
}

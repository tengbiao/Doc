using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Com.Web.Common;

namespace Com.Web.Repository
{
    public abstract class BaseRepository<TEntity> where TEntity : new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IDbConnection GetConnection()
        {
            IDbConnection connection = new SqlConnection(ConfigHelper.GetDbConnectionStr("DbConnectionStr"));
            connection.Open();
            return connection;
        }
    }
}

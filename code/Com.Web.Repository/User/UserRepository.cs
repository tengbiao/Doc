using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Com.Web.Model;
using System.Data;
using Com.Web.Model.Model;

namespace Com.Web.Repository.User
{
    public class UserRepository : BaseRepository<T_User>
    {
        /// <summary>
        /// 得到一个对象实体,登录所需
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<T_User> GetLoginModel(string userName)
        {
            using (IDbConnection conn = GetConnection())
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id,username,password  from t_member ");
                strSql.Append(" where username=@username");
                return await conn.QueryFirstOrDefaultAsync<T_User>(strSql.ToString(), new { username = userName });
            }
        }
    }
}

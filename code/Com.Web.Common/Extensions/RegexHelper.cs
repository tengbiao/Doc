using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Com.Web.Common
{
    public static class RegexHelper
    {
        /// <summary>
        /// 截取IP格式
        /// </summary>
        public static string GetIP(this string ip)
        {
            Regex reip = new Regex("(\\d+).(\\d+).(\\d+).(\\d+)");
            return reip.Replace(ip, "$1.$2.$3.$4");
        }
    }
}

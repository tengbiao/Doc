using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Web.Common
{
    public static class JsonHelper
    {
        private static JsonSerializerSettings jSetting = new JsonSerializerSettings();
        static JsonHelper()
        {
            jSetting.Formatting = Formatting.Indented;
            jSetting.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            jSetting.NullValueHandling = NullValueHandling.Ignore;
            jSetting.Converters.Add(new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }

        /// <summary>
        /// 将对象转换成json字符串
        /// </summary>
        /// <param name="obj">需要转换的对象</param>
        /// <param name="ignoreNull">是否排除NULL属性，默认不排除</param>
        /// <returns></returns>
        public static string ToJson(this object obj, bool ignoreNull = false)
        {
            try
            {
                if (ignoreNull)
                {
                    jSetting.NullValueHandling = NullValueHandling.Ignore;
                    return JsonConvert.SerializeObject(obj, jSetting);
                }
                else
                {
                    jSetting.NullValueHandling = NullValueHandling.Include;
                    return JsonConvert.SerializeObject(obj, jSetting);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 将json字符串序列化成对象
        /// </summary>
        /// <typeparam name="T">序列化的目标对象</typeparam>
        /// <param name="jsonstr">json字符串</param>
        /// <returns></returns>
        public static T ToObject<T>(this string jsonstr)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(jsonstr, jSetting);
            }
            catch
            {
                return default(T);
            }
        }
    }
}

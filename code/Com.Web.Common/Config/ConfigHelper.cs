using System;
using System.Configuration;
using System.Web;

namespace Com.Web.Common
{
    /// <summary>
    /// web.config操作类
    /// Copyright (C) Ytx_Admin
    /// </summary>
    public sealed class ConfigHelper
    {
        /// <summary>
        /// 获取数据库连接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDbConnectionStr(string key)
        {
            string CacheKey = "Connection-" + key;
            object objModel = HttpRuntime.Cache[CacheKey];
            if (objModel == null)
            {
                try
                {
                    objModel = ConfigurationManager.ConnectionStrings[key];
                    if (objModel != null)
                    {
                        HttpRuntime.Cache.Insert(CacheKey, objModel, null, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return objModel.ToString();
        }

        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSeting(string key)
        {
            string CacheKey = "AppSettings-" + key;
            object objModel = HttpRuntime.Cache[CacheKey];
            if (objModel == null)
            {
                try
                {
                    objModel = ConfigurationManager.AppSettings[key];
                    if (objModel != null)
                    {
                        HttpRuntime.Cache.Insert(CacheKey, objModel, null, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return objModel.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetAppSeting<T>(string key)
        {
            try
            {
                var appsetting = GetAppSeting(key);
                if (!string.IsNullOrEmpty(appsetting))
                {
                    return (T)Convert.ChangeType(appsetting, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
            catch
            {
                return default(T);
            }
        }
    }
}

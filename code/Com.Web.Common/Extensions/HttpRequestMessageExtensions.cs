using System;
using System.Net.Http;
using System.Web;

namespace Com.Web.Common
{
    public static class HttpRequestMessageExtensions
    {
        private const string HttpContext = "MS_HttpContext";
        private const string RemoteEndpointMessage = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OwinContext = "MS_OwinContext";

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            // Web-hosting. Needs reference to System.Web.dll
            if (request.Properties.ContainsKey(HttpContext))
            {
                dynamic ctx = request.Properties[HttpContext];
                if (ctx != null)
                {
                    return GetRealIp(ctx.Request);
                }
            }
            // Self-hosting. Needs reference to System.ServiceModel.dll. 
            if (request.Properties.ContainsKey(RemoteEndpointMessage))
            {
                dynamic remoteEndpoint = request.Properties[RemoteEndpointMessage];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }
            // Self-hosting using Owin. Needs reference to Microsoft.Owin.dll. 
            if (request.Properties.ContainsKey(OwinContext))
            {
                dynamic owinContext = request.Properties[OwinContext];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }
            return "0.0.0.0";
        }


        /// <summary>
        /// 获取用户的IP地址-全
        /// Add by Lin at 2015.1.4
        /// </summary>
        private static string GetRealIp(HttpRequestWrapper request)
        {
            string user_IP = request.Headers["X-Forwarded-For"];
            if (user_IP != null && user_IP.ToLower() != "unknown")
            {
                //X-Forwarded-For: client1, proxy1, proxy2    
                string[] arrIp = user_IP.Split(',');
                user_IP = arrIp[0];
                if (arrIp.Length > 1)
                {    //如果第一组IP是10和168开头还有172.16-172.31（第二码区间在16-31之间）的话，就取第二组IP
                    if (user_IP.IndexOf("10.") == 0 || user_IP.IndexOf("192.168.") == 0 || (user_IP.IndexOf("172.") == 0 && (user_IP.Split('.').Length > 1 && Convert.ToInt32(user_IP.Split('.')[1]) > 15 && Convert.ToInt32(user_IP.Split('.')[1]) < 32)))
                    {
                        user_IP = arrIp[1];
                    }
                }
            }
            else if (request.ServerVariables["REMOTE_ADDR"] != null && request.ServerVariables["REMOTE_ADDR"].ToLower() != "unknown")
            {
                user_IP = request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                user_IP = request.UserHostAddress;
            }
            if (user_IP.Length > 15)
            {
                user_IP = user_IP.Substring(0, 15);
            }
            return RegexHelper.GetIP(user_IP);
        }
    }
}

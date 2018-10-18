using Newtonsoft.Json.Converters;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Com.Web.Api.App_Start.Handler;

namespace Com.Web.Api
{
    /// <summary>
    /// 
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //设置默认的为Swagger
            config.Routes.MapHttpRoute(
               name: "swagger_root",
               routeTemplate: "",
               defaults: null,
               constraints: null,
               handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //添加自定义异常处理返回
            config.MessageHandlers.Add(new CustomErrorMessageHandler());
            //Action Filter
            config.Filters.Add(new GlobalActionFilterAttribute());

            var format = GlobalConfiguration.Configuration.Formatters;
            format.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter
            {
                DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
            });
            //json序列化清楚空值
            format.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            format.XmlFormatter.UseXmlSerializer = true;
        }
    }
}

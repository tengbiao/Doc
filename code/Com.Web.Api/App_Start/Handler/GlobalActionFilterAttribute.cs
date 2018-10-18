using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Com.Web.Api.App_Start.Handler
{
    /// <summary>
    /// 全局Action Filter
    /// </summary>
    public class GlobalActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            //验证数据模型
            if (actionContext.ActionDescriptor.GetCustomAttributes<BypassModelStateValidationAttribute>().Any() ||
                actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<BypassModelStateValidationAttribute>().Any())
            {
                return base.OnActionExecutingAsync(actionContext, cancellationToken);
            }
            if (actionContext.ModelState.IsValid == false)
            {
                var errors = actionContext.ModelState.Values.Select(r => r.Errors.Select(e => e.ErrorMessage).FirstOrDefault()).ToList();
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest,string.Join(",", errors));
            }
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}

namespace Com.Web.Api
{
    /// <summary>
    /// 
    /// </summary>

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public sealed class BypassModelStateValidationAttribute : Attribute
    {

    }
}
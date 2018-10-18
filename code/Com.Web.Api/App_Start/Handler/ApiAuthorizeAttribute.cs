using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Com.Web.Common;

namespace Com.Web.Api.App_Start.Handler
{
    /// <summary>
    /// 接口请求验证权限
    /// </summary>
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            //从http请求的头里面获取身份验证信息，验证是否是请求发起方的ticket
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && (authorization.Parameter != null))
            {
                //解密用户ticket,并校验用户名密码是否匹配
                var token = authorization.Parameter;
                var validate = await ValidateToken(actionContext.Request, token);
                if (validate.HasValue && validate.Value.Value)
                {
                    var currentPrincipal = new GenericPrincipal(new GenericIdentity(validate.Value.Key), new string[] { });
                    Thread.CurrentPrincipal = currentPrincipal;
                    actionContext.RequestContext.Principal = Thread.CurrentPrincipal;
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
            else
            {
                var isAnonymous = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
                if (isAnonymous)
                    await base.OnAuthorizationAsync(actionContext, cancellationToken);
                else
                    HandleUnauthorizedRequest(actionContext);
            }
        }

        /// <summary>
        /// 验证Token是否有效
        /// </summary>
        /// <param name="token">
        /// Token 规则  DES(会员账号&MD5(ip&logintime&token加密key))
        /// </param>
        /// <returns></returns>
        private async Task<KeyValuePair<string, bool>?> ValidateToken(HttpRequestMessage request, string token)
        {
            //此处需要查询数据库的token表，或者自行使用redis实现都可以
            var userToken = await Task.Run(() => { return new { userName = "test", token = "qwe123456" }; });
            if (userToken != null)
            {
                return new KeyValuePair<string, bool>(userToken.userName, userToken.token == token);
            }
            return null;
        }
    }
}
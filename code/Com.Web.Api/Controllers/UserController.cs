using Com.Web.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Com.Web.Api.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : ApiBaseController
    {
        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="loginModel">用户帐密</param>
        /// <remarks>用户登录接口</remarks>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ApiResult<LoginResModel>))]
        [AllowAnonymous]
        [Route("api/user/login")]
        public async Task<IHttpActionResult> Login(LoginReqModel loginModel)
        {
            if (loginModel == null)
                return BadRequest("请求格式有误");
            if (loginModel.password == null || loginModel.password.Length == 0
                || loginModel.user != "test" || loginModel.password != "test123")
                return BadRequest("用户密码错误");
            return await Task.Run(() =>
             {
                 var loginTime = DateTime.Now;
                 var token = "qwe123456";
                 var res = new LoginResModel()
                 {
                     UserName = loginModel.user,
                     Level = 1,
                     Money = 0,
                     Token = token
                 };
                 return SuccessResult(res);
             });
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ApiResult<List<String>>))]
        [Route("api/user/getusers")]
        public async Task<IHttpActionResult> GetUsers()
        {
            return await Task.Run(() =>
             {
                 var dd = new List<string>() { "user1", "user2", "user3" };
                 dd.Add(User.Identity.Name);
                 return SuccessResult(dd);
             });
        }

    }
}

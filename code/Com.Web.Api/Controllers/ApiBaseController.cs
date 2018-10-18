using Com.Web.Api.App_Start.Handler;
using Com.Web.Model.ViewModel;
using System.Net;
using System.Web.Http;

namespace Com.Web.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiAuthorize]
    public class ApiBaseController : ApiController
    {       

        /// <summary>
        /// 公共返回成功方式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        public IHttpActionResult SuccessResult(dynamic data = null)
        {
            return Ok(new ApiResult<dynamic>() { ResultCode = ApiResultCode.Success, Data = data });
            //return Content(HttpStatusCode.OK, new ApiResult<dynamic>() { ResultCode = ApiResultCode.Success, Data = data });
        }

        /// <summary>
        /// 公共错误返回方式
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        public IHttpActionResult ErrorResult(HttpStatusCode statusCode, string message = null, dynamic data = null)
        {
            return Content(statusCode, new ApiResult<dynamic>() { ResultCode = (ApiResultCode)statusCode, Message = message, Data = data });
        }

        /// <summary>
        /// 公共返回成功方式
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        public ApiResult<T> Success<T>(T data)
        {
            return new ApiResult<T>() { ResultCode = ApiResultCode.Success, Data = data };
        }

        /// <summary>
        /// 公共错误返回方式
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [NonAction]
        public ApiResult<T> Error<T>(ApiResultCode resultCode, string message = null, dynamic data = null)
        {
            return new ApiResult<T>() { ResultCode = resultCode, Message = message, Data = data };
        }
    }
}

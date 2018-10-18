using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using Com.Web.Model.ViewModel;

namespace Com.Web.Api.App_Start.Handler
{
    /// <summary>
    /// API自定义错误消息处理委托类。
    /// </summary>
    public class CustomErrorMessageHandler : DelegatingHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith((responseToCompleteTask) =>
            {
                HttpResponseMessage response = responseToCompleteTask.Result;
                HttpError error;
                if (response.TryGetContentValue(out error))
                {
                    var resModel = new ApiResult() { ResultCode = (ApiResultCode)response.StatusCode };
                    resModel.Message = error.ExceptionMessage ?? error.Message;

                    var isXml = request.Headers.Contains("Accept") && request.Headers.Accept.Count(q => q.MediaType.ToLower().Contains("xml")) > 0;
                    MediaTypeFormatter mediaType = isXml ? new XmlMediaTypeFormatter() : new JsonMediaTypeFormatter() as MediaTypeFormatter;

                    return request.CreateResponse(HttpStatusCode.OK, resModel, mediaType);
                }
                else
                {
                    return response;
                }
            });
        }
    }
}
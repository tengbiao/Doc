using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Com.Web.Model.ViewModel
{
    /// <summary>
    /// 用户登录
    /// </summary>
    [DataContract(Namespace = "")]
    public class LoginReqModel
    {
        /// <summary>
        /// 会员登录账号
        /// </summary>       
        [StringLength(11, MinimumLength = 4, ErrorMessage = "会员账号不存在")]
        [Required(ErrorMessage = "请填写登录帐号！")]
        [DataMember]
        public string user { set; get; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "请填写登录密码！")]
        [DataMember]
        public string password { set; get; }
    }

    /// <summary>
    /// 用户登录信息返回
    /// </summary>
    public class LoginResModel
    {
        /// <summary>
        /// 会员账号
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 会员等级
        /// </summary>
        public int Level { set; get; }
        /// <summary>
        /// 会员余额
        /// </summary>
        public int Money { set; get; }
        /// <summary>
        /// 会员Token,用于访问需要验证的接口 Header=>{"key":"Authorization","value":"Basic Token"}
        /// </summary>
        public string Token { set; get; }
    }
}
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiOwin.Controllers
{

    /// <summary>
    /// 权限配置模型
    /// </summary>
    public class TokenAuthConfigurationModel
    {
        /// <summary>
        /// token的key
        /// </summary>
        public SymmetricSecurityKey SecurityKey { get; set; }

        /// <summary>
        /// 是否用户
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 认证的值
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// token的验证
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; }
    }
}
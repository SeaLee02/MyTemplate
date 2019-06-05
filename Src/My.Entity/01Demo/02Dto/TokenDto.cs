namespace My.Entity.Demo.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 认证授权
    /// </summary>
    public class AuthenticateInDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string PassWord { get; set; }

    }


    /// <summary>
    /// 认证返回值
    /// </summary>
    public class AuthenticateOutDto
    {
        /// <summary>
        /// token值
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public int Expire { get; set; }

    }
}

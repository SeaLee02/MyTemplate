namespace My.Entity.Demo.Dto
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 创建用户
    /// </summary>
    public class CreateJwtUserInDto
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
    /// 更新用户
    /// </summary>
    public class UpdateJwtUserInDto
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public string JwtUserGuid { get; set; }

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
    /// 删除用户
    /// </summary>
    public class DeleteJwtUserInDto
    {

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public string JwtUserGuid { get; set; }

    }


    /// <summary>
    /// 用户名获取用户
    /// </summary>
    public class GetModelByUserNameInDto
    {

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }

    }




}

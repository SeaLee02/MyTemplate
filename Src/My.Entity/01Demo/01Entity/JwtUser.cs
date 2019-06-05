namespace My.Entity.Demo
{
    using System;
    using System.ComponentModel;   
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Jwt用户表
    /// </summary>
    [Table("JwtUser")]
    public class JwtUser
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Column("JwtUserGuid")]
        [Display(Name = "Jwt用户表主键", Description = "Jwt用户表主键")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Description = "用户名", Name = "用户名")]
		[Column("UserName")]
        [StringLength(128)]
        public string  UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Description = "密码", Name = "密码")]
		[Column("PassWord")]
        [StringLength(128)]
        public string  PassWord { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Description = "创建时间", Name = "创建时间")]
		[Column("CreateTime")]
        public DateTime?  CreateTime { get; set; }

    }

}

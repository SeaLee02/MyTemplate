namespace My.Entity.Demo.View
{
    using System;   
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 用户2角色关系视图
    /// </summary>
    [Table("View_User2Role")]
    public class ViewUser2Role
    {
		/// <summary>
        /// 主键
        /// </summary>
        [Display(Description = "主键", Name = "主键")]
        [Column("User2RoleGuid")]
        public Guid  Id { get; set; }

		/// <summary>
        /// 用户主键
        /// </summary>
        [Display(Description = "用户主键", Name = "用户主键")]
        [Column("JwtUserGuid")]
        public Guid?  JwtUserGuid { get; set; }

		/// <summary>
        /// 角色主键
        /// </summary>
        [Display(Description = "角色主键", Name = "角色主键")]
        [Column("JwtRoleGuid")]
        public Guid?  JwtRoleGuid { get; set; }

		/// <summary>
        /// 用户名
        /// </summary>
        [Display(Description = "用户名", Name = "用户名")]
        [Column("UserName")]
        [StringLength(128)]
        public string  UserName { get; set; }

		/// <summary>
        /// 用户密码
        /// </summary>
        [Display(Description = "用户密码", Name = "用户密码")]
        [Column("PassWord")]
        [StringLength(128)]
        public string  PassWord { get; set; }

		/// <summary>
        /// 角色名
        /// </summary>
        [Display(Description = "角色名", Name = "角色名")]
        [Column("RoleName")]
        [StringLength(128)]
        public string  RoleName { get; set; }

		/// <summary>
        /// 角色code
        /// </summary>
        [Display(Description = "角色code", Name = "角色code")]
        [Column("RoleCode")]
        [StringLength(30)]
        public string  RoleCode { get; set; }

		/// <summary>
        /// 创建时间
        /// </summary>
        [Display(Description = "创建时间", Name = "创建时间")]
        [Column("CreateTime")]
        public DateTime?  CreateTime { get; set; }

    }
}


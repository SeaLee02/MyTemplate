namespace My.Entity.Demo
{
    using System;
    using System.ComponentModel;   
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// 用户2角色关系
    /// </summary>
    [Table("JwtUser2Role")]
    public class JwtUser2Role
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Column("User2RoleGuid")]
        [Display(Name = "用户2角色关系主键", Description = "用户2角色关系主键")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

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
        /// 创建时间
        /// </summary>
        [Display(Description = "创建时间", Name = "创建时间")]
		[Column("CreateTime")]
        public DateTime?  CreateTime { get; set; }

    }

}

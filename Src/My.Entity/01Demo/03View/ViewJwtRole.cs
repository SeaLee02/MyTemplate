namespace My.Entity.Demo.View
{
    using System;   
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Jwt角色表
    /// </summary>
    [Table("View_JwtRole")]
    public class ViewJwtRole
    {
		/// <summary>
        /// 角色主键
        /// </summary>
        [Display(Description = "角色主键", Name = "角色主键")]
        [Column("JwtRoleGuid")]
        public Guid  Id { get; set; }

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


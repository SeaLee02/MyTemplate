namespace My.Entity.Demo
{
    using System;
    using System.ComponentModel;   
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Jwt角色表
    /// </summary>
    [Table("JwtRole")]
    public class JwtRole
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Column("JwtRoleGuid")]
        [Display(Name = "Jwt角色表主键", Description = "Jwt角色表主键")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [Display(Description = "角色名称", Name = "角色名称")]
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


namespace My.DataAccess.Framework
{
    using System.Data.Entity;
    using My.Entity.Demo;

    /// <summary>
    /// 数据库对应表
    /// </summary>
	public partial class MyDbContext
    { 
		/// <summary>
        /// Jwt角色表
        /// </summary>
		public DbSet<JwtRole> JwtRole { get; set; }
	 
		/// <summary>
        /// Jwt用户表
        /// </summary>
		public DbSet<JwtUser> JwtUser { get; set; }
	 
		/// <summary>
        /// 用户2角色关系
        /// </summary>
		public DbSet<JwtUser2Role> JwtUser2Role { get; set; }
	 
	}
}
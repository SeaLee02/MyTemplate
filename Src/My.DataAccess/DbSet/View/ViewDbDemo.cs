
namespace My.DataAccess.Framework
{
    using System.Data.Entity;
    using My.Entity.Demo.View;

    /// <summary>
    /// 数据库对应表
    /// </summary>
	public partial class MyDbContext
    { 
		/// <summary>
        /// Jwt角色表
        /// </summary>
		public DbSet<ViewJwtRole> ViewJwtRole { get; set; }
	 
		/// <summary>
        /// Jwt用户表
        /// </summary>
		public DbSet<ViewJwtUser> ViewJwtUser { get; set; }
	 
		/// <summary>
        /// 用户2角色关系视图
        /// </summary>
		public DbSet<ViewUser2Role> ViewUser2Role { get; set; }
	 
	}
}
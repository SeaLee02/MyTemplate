namespace My.Application.Repositories.Demo
{
    using My.DataAccess.Framework;
    using My.Entity.Demo;
    using My.Entity.Demo.Dto;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

	/// <summary>
    /// Jwt角色表管理仓储
    /// </summary>
    public class JwtRoleRepository : IJwtRoleRepository
    {
		private readonly MyDbContext db = new MyDbContext();

         
	}
}

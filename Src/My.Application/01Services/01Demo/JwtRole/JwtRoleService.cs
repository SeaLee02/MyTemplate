namespace My.Application.Services.Demo
{
    using My.Application.Repositories.Demo;
    using My.Entity.Demo;
    using My.Entity.Demo.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Jwt角色表应用层服务
    /// </summary>
    public class JwtRoleService : IJwtRoleService
    {
        private readonly IJwtRoleRepository _jwtRoleRepository;

        public JwtRoleService(IJwtRoleRepository jwtRoleRepository)
        {
            this._jwtRoleRepository = jwtRoleRepository;
        }

    }
}

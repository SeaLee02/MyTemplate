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
    /// 用户应用层服务
    /// </summary>
    public class JwtUserService : IJwtUserService
    {
        private readonly IJwtUserRepository _jwtUserRepository;

        public JwtUserService(IJwtUserRepository jwtUserRepository)
        {
            this._jwtUserRepository = jwtUserRepository;
        }

        /// <summary>
        /// 全部用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<JwtUser>> GetListAsync()
        {
            return await this._jwtUserRepository.GetListAsync();
        }

        /// <summary>
        ///  用户名和密码获取用户
        /// </summary>
        /// <param name="inDto">inDto</param>
        /// <returns>OutDto</returns>
        public async Task<JwtUser> GetJwtUserAsync(AuthenticateInDto inDto)
        {
            return await _jwtUserRepository.GetJwtUserAsync(inDto);
        }



        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="inDto"></param>
        /// <returns></returns>
        public async Task<string> CreateJwtUserAsync(CreateJwtUserInDto inDto)
        {
            return await this._jwtUserRepository.CreateJwtUserAsync(inDto);
        }

        /// <summary>
        /// 更新用户名获取用户
        /// </summary>
        /// <param name="inDto"></param>
        /// <returns></returns>
        public async Task<bool> GetModelByUserNameAsync(GetModelByUserNameInDto inDto)
        {
            return await this._jwtUserRepository.GetModelByUserNameAsync(inDto);
        }
    }
}

namespace My.Application.Services.Demo
{
    using My.Entity.Demo;
    using My.Entity.Demo.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 用户应用层服务接口
    /// </summary>
    public interface IJwtUserService
    {
        /// <summary>
        /// 全部用户
        /// </summary>
        /// <returns></returns>
        Task<List<JwtUser>> GetListAsync();

        /// <summary>
        ///  用户名和密码获取用户
        /// </summary>
        /// <param name="inDto">inDto</param>
        /// <returns>OutDto</returns>
        Task<JwtUser> GetJwtUserAsync(AuthenticateInDto inDto);


        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="inDto"></param>
        /// <returns></returns>
        Task<string> CreateJwtUserAsync(CreateJwtUserInDto inDto);

        /// <summary>
        /// 更新用户名获取用户
        /// </summary>
        /// <param name="inDto"></param>
        /// <returns></returns>
        Task<bool> GetModelByUserNameAsync(GetModelByUserNameInDto inDto);
    }
}

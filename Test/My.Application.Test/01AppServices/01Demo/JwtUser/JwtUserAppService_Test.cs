namespace My.Application.Test.AppServices.Demo
{
    using My.Application.Services.Demo;
    using My.Entity.Demo;
    using My.Entity.Demo.Dto;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// Jwt用户表应用层服务测试
    /// </summary>
    public class JwtUserAppService_Test
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        private readonly IJwtUserService _jwtUserService;

        public JwtUserAppService_Test(IJwtUserService jwtUserService)
        {
            this._jwtUserService = jwtUserService;
        }


        /// <summary>
        /// 全部用户
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetListAsync_Test()
        {
            List<JwtUser> list = await this._jwtUserService.GetListAsync();
        }

        /// <summary>
        ///  用户名和密码获取用户
        /// </summary>
        /// <param name="inDto">inDto</param>
        /// <returns>OutDto</returns>
        [Fact]
        public async Task GetJwtUserAsync_Test()
        {
            AuthenticateInDto inDto = new AuthenticateInDto
            {
                UserName = "sealee",
                PassWord = "123"
            };
            JwtUser model = await this._jwtUserService.GetJwtUserAsync(inDto);
        }


        //添加删除修改，都需要新建一个类进行测试

    }
}

using My.Application.Services.Demo;
using My.Entity.Demo;
using My.Entity.Demo.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiOwin.App_Start;

namespace WebApiOwin.Controllers
{
    /// <summary>
    /// jwt用户管理
    /// </summary>
    [MyVersionAttribute("JwtUser")]
    public class JwtUserController : ApiController
    {

        private readonly IJwtUserService _jwtUserService;
        public JwtUserController(IJwtUserService jwtUserService)
        {
            this._jwtUserService = jwtUserService;
        }


        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<List<JwtUser>> JwtUsersListAsync()
        {
            List<JwtUser> list = await this._jwtUserService.GetListAsync();
            return await Task.FromResult(list);
        }



        /// <summary>
        /// 创建jwt用户
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<string> CreateJwtUserAsync(CreateJwtUserInDto inDto)
        {

            bool isB = await this._jwtUserService.GetModelByUserNameAsync(new GetModelByUserNameInDto { UserName = inDto.UserName });
            if (isB)
            {
                throw new MyFriendlyException($"用户名为:{inDto.UserName} 已存在");
            }

            string guid = await this._jwtUserService.CreateJwtUserAsync(inDto);
            return await Task.FromResult(guid);
        }



    }
}

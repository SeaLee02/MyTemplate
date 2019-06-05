using My.Application.Services.Demo;
using My.Entity.Demo;
using My.Entity.Demo.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiOwin.App_Start;
using WebApiOwin.Model;

namespace WebApiOwin.Controllers
{
    /// <summary>
    /// 授权
    /// </summary>
    [MyVersionAttribute("Token")]
    public class TokenController : ApiController
    {
        private readonly IJwtUserService _jwtUserService;
        public TokenController(IJwtUserService jwtUserService)
        {
            this._jwtUserService = jwtUserService;
        }


        /// <summary>
        ///  获取token
        /// </summary>
        /// <param name="inDto">请求参数</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<AuthenticateOutDto> AuthenticateAsync(AuthenticateInDto inDto)
        {
            JwtUser model = await this._jwtUserService.GetJwtUserAsync(inDto);
            if (model == null)
            {
                throw new MyFriendlyException("账号或密码不对");
            }
            string role = "admin";
            string access_token = await CreateAccessToken(role);
            //string refresh_token = await CreateRefreshToken();
            AuthenticateOutDto outDto = new AuthenticateOutDto()
            {
                AccessToken = access_token,
                //Refreshtoken = DESEncrypt.Encrypt(refresh_token, "sealee"),
                Expire = (int)TimeSpan.FromMinutes(Convert.ToDouble(5)).TotalSeconds  // TimeSpan.FromMinutes(Convert.ToDouble(5)),
            };
            return await Task.FromResult(outDto);
        }



        /// <summary>
        /// 创建Token
        /// </summary>
        /// <returns></returns>
        private static async Task<string> CreateAccessToken(string role)
        {
            TokenAuthConfigurationModel configuration = new TokenAuthConfigurationModel();
            configuration.SecurityKey = ConfigHelper.GetSymmetricSecurityKey();
            configuration.Issuer = ConfigHelper.GetIssuer();
            configuration.Audience = ConfigHelper.GetAudience();
            configuration.SigningCredentials = ConfigHelper.GetSigningCredentials();
            configuration.Expiration = TimeSpan.FromMinutes(Convert.ToDouble(5));
            DateTime now = DateTime.UtcNow;

            ClaimsIdentity identity = new ClaimsIdentity("JwtBearer");
            List<Claim> claims = identity.Claims.ToList();
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "sealee"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            });
            ////添加角色
            //claims.Add(new Claim(ClaimTypes.Role, "sealee"));
            claims.Add(new Claim(ClaimTypes.Role, role));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: configuration.Issuer,
                audience: configuration.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(configuration.Expiration),
                signingCredentials: configuration.SigningCredentials
            );
            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return await Task.FromResult(token);
        }




        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IHttpActionResult> Test()
        {
            return await Task.FromResult(Ok(new { errcode = 0, msg = "授权成功" }));
        }


    }
}

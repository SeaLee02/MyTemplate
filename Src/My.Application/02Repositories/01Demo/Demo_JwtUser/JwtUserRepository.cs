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
    /// jwt用户管理仓储
    /// </summary>
    public class JwtUserRepository : IJwtUserRepository
    {
        private readonly MyDbContext db = new MyDbContext();

        /// <summary>
        /// 全部用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<JwtUser>> GetListAsync()
        {
            return await Task.FromResult(db.JwtUser.AsNoTracking().ToList());
        }


        /// <summary>
        ///  用户名和密码获取用户
        /// </summary>
        /// <param name="inDto">inDto</param>
        /// <returns>OutDto</returns>
        public async Task<JwtUser> GetJwtUserAsync(AuthenticateInDto inDto)
        {
            JwtUser model = db.JwtUser.AsNoTracking().FirstOrDefault(x => x.UserName == inDto.UserName && x.PassWord == inDto.PassWord);
            return await Task.FromResult(model ?? null);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="inDto"></param>
        /// <returns></returns>
        public async Task<string> CreateJwtUserAsync(CreateJwtUserInDto inDto)
        {
            JwtUser jwtUser = new JwtUser();
            jwtUser.UserName = inDto.UserName;
            jwtUser.PassWord = inDto.PassWord;
            jwtUser.CreateTime = DateTime.Now;
            db.Entry(jwtUser).State = EntityState.Added;
            await db.SaveChangesAsync();
            return await Task.FromResult(jwtUser.Id.ToString());
        }

        /// <summary>
        /// 更新用户名获取用户
        /// </summary>
        /// <param name="inDto"></param>
        /// <returns></returns>
        public async Task<bool> GetModelByUserNameAsync(GetModelByUserNameInDto inDto)
        {
            bool isHas = await db.JwtUser.AsNoTracking().AnyAsync(x => x.UserName == inDto.UserName);
            return await Task.FromResult(isHas);
        }

    }
}

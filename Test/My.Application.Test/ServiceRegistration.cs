using Autofac;
using My.Application.Repositories.Demo;
using My.Application.Services.Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.Test
{

    public class ServiceRegistration : Module
    {
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JwtUserRepository>().As<IJwtUserRepository>();
            builder.RegisterType<JwtUserService>().As<IJwtUserService>();
        }
    }
}

using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using My.Application.Repositories.Demo;
using My.Application.Services.Demo;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiOwin.App_Start;
using WebApiOwin.Model;

[assembly: OwinStartup(typeof(WebApiOwin.Startup))]

namespace WebApiOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            WebApiConfig(config);

            //使用log4.net
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));

            ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //注册依赖注入
            ContainerBuilder builder = new ContainerBuilder();
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<JwtUserRepository>().As<IJwtUserRepository>();
            builder.RegisterType<JwtUserService>().As<IJwtUserService>();

            // Run other optional steps, like registering filters,
            // per-controller-type services, etc., then set the dependency resolver
            // to be Autofac.
            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // OWIN WEB API SETUP:

            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

            app.UseWebApi(config);
        }


        /// <summary>
        /// webapi配置
        /// </summary>
        /// <param name="config"></param>
        public static void WebApiConfig(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();
            //多添加一个action
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // 干掉XML序列化器   两种都可以
            //GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //配置json数据格式
            JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;
            //忽略循环引用，如果设置为Error，则遇到循环引用的时候报错（建议设置为Error，这样更规范）
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Error;
            //日期格式化，默认的格式也不好看
            jsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            // 对 JSON 数据使用混合大小写。跟属性名同样的大小.输出
            jsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //json中属性开头字母小写的驼峰命名
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //模型验证
            config.Filters.Add(new MyActionFilterAttribute());
            //错误处理
            config.Filters.Add(new MyErrorFilter());

            //文档配置
            DocConfig.Register(config);
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureOAuth(IAppBuilder app)
        {

            app.UseJwtBearerAuthentication(
              new JwtBearerAuthenticationOptions
              {

                  //RequireClaim
                  //AllowedAudiences = new string[] { };
                  AuthenticationMode = AuthenticationMode.Active,
                  TokenValidationParameters = new TokenValidationParameters()
                  {
                      //验证秘钥
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = ConfigHelper.GetSymmetricSecurityKey(),

                      //验证Audience
                      ValidateAudience = true,
                      ValidAudience = ConfigHelper.GetAudience(),

                      //验证Issuer
                      ValidateIssuer = true,
                      ValidIssuer = ConfigHelper.GetIssuer(),

                      //验证过期时间
                      ValidateLifetime = true,

                      //验证类型
                      AuthenticationType = "JwtBearer"
                  }
              });
        }

    }
}

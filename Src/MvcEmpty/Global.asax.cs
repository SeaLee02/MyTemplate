using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcEmpty
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //注册所有的区域
            AreaRegistration.RegisterAllAreas();
            //资源绑定配置
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            //过滤配置
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            //路由配置
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //使用log4net
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
        }
    }
}

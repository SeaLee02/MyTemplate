using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcEmpty
{
    /// <summary>
    /// 路由配置
    /// </summary>
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //属性路由
            routes.MapMvcAttributeRoutes();

            //多个路由 从上到下满足，难满足的写在前面
            //  routes.MapRoute(
            //    name: "WebRoute",
            //    url: "{web}/{controller}/{action}.html/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //    namespace
            //);


            ////传统路由   设置区域未起始页
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Test", action = "Index", id = UrlParameter.Optional },
            //    namespaces: new string[] { "MvcEmpty.Areas.Web.Controllers" }
            //).DataTokens.Add("Area", "Web");

            //defaults，namespaces，DataTokens  改这三个


            //传统路由
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
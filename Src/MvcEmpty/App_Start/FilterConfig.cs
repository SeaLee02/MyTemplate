using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEmpty
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //添加错误过滤器
            filters.Add(new HandleErrorAttribute());
            //添加认证过滤
            filters.Add(new MyAuthorize());
            //添加方法过滤
            filters.Add(new MyActionFilter());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEmpty
{
    /// <summary>
    /// 认证过滤器
    /// </summary>
    public class MyAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //逻辑验证
            string[] users = Users.Split(',');
            string[] roles = Roles.Split(',');

            return true;
            //return base.AuthorizeCore(httpContext);
        }

        /// <summary>
        /// AuthorizeCore 返回fasle会进到这里
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            else
            {
                //跳转页面
                filterContext.Result = new RedirectResult("");
            }
        }
    }
}
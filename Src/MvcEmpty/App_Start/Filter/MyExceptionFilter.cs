using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEmpty
{
    /// <summary>
    /// 自定义异常处理
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class MyExceptionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled) //如果没有处理异常
            {
                Type t = filterContext.Controller.GetType();
                //使用log4net
                ILog log = LogManager.GetLogger(t);//实例
                //控制器
                string controllerName = (string)filterContext.RouteData.Values["controller"];
                //方法
                string actionName = (string)filterContext.RouteData.Values["action"];

                string msgTemplate = $"在执行 controller[{controllerName}] 的 action[{actionName}] 时产生异常";
                log.Error(msgTemplate, filterContext.Exception);
                if (filterContext.HttpContext.Request.IsAjaxRequest())//检查请求头
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = new { errcode = 1, errmsg = "系统出现异常，请联系管理员", debugmsg = filterContext.Exception.Message }
                    };
                }
                else
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "~/Views/Shared/Error.cshtml",
                        ViewData = new ViewDataDictionary<string>(filterContext.Exception.Message)
                    };
                }
                filterContext.ExceptionHandled = true;
            }
        }

    }
}
using log4net;
using Newtonsoft.Json;
using Sealee.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiOwin.App_Start
{
    public class MyErrorFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpActionContext context = actionExecutedContext.ActionContext;
            //参数列表
            Dictionary<string, object> paramsList = context.ActionArguments;
            //业务逻辑错误
            if (actionExecutedContext.Exception is MyFriendlyException)
            {
                object obj2 = new
                {
                    errcode = -1,
                    errmsg = actionExecutedContext.Exception.Message,
                    param = paramsList
                };

                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(obj2), Encoding.UTF8, "application/json")
                };
            }
            else
            {
                Type t = context.ControllerContext.Controller.GetType(); //得到控制器的类型
                string controllerDes = t.GetDescription(); //控制器的描述
                string controllerName = context.ActionDescriptor.ControllerDescriptor.ControllerName;//控制器的名称
                string actionName = context.ActionDescriptor.ActionName;//方法名
                string actionDes = actionName.GetDescriptionByMethod(t);//方法描述      
                ILog log = log4net.LogManager.GetLogger(t);//实例
                string parsmsJson = JsonConvert.SerializeObject(paramsList);
                log.Error($"{actionName},{parsmsJson}", actionExecutedContext.Exception);

                object obj = new
                {
                    errcode = -1,
                    errmsg = actionExecutedContext.Exception,
                    param = paramsList
                };

                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                };
            }
            base.OnException(actionExecutedContext);
        }
    }
}
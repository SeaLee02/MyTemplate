using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiOwin.App_Start
{
    /// <summary>
    /// 自定抛出错误，一般为业务错误
    /// </summary>
    public class MyFriendlyException : Exception
    {

        public MyFriendlyException() : base()
        {

        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        public MyFriendlyException(string message) : base(message)
        {

        }

        /// <summary>
        /// 错误信息带异常
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="ex">异常</param>
        public MyFriendlyException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
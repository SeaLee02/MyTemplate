using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiOwin.App_Start;

namespace WebApiOwin.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    [MyVersionAttribute("JwtUser")]
    public class TestController : ApiController
    {

        /// <summary>
        /// 测试get方法
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IHttpActionResult> TestAsync()
        {

            return await Task.FromResult(Ok(new { errcode = 0, msg = "Hello Word" }));
        }
        /// <summary>
        /// 测试post方法
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Test2Async()
        {

            return await Task.FromResult(Ok(new { errcode = 0, msg = "Hello Word" }));
        }

    }
}

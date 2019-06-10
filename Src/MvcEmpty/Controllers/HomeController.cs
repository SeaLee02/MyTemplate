using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcEmpty.Controllers
{
    //属性路由
    //[RoutePrefix("Home")]
    //[Route("{action}")]
    public class HomeController : Controller
    {
        private readonly List<Student> list = new List<Student>();
        public HomeController()
        {
            list.Add(new Student { ID = 1, UserName = "sealee", Age = 18 });
            list.Add(new Student { ID = 2, UserName = "张三", Age = 21 });
            list.Add(new Student { ID = 3, UserName = "李四", Age = 22 });
            list.Add(new Student { ID = 4, UserName = "王五", Age = 23 });
        }

        // GET: Home
        //[Route("Index")]
        public async Task<ActionResult> Index()
        {

            ViewBag.json = JsonConvert.SerializeObject(list);
            return View();
        }

        /// <summary>
        /// 返回json字符串
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> ListData()
        {
            var data = new
            {
                errcode = 0,
                data = list
            };
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Error, //忽略循环引用，如果设置为Error，则遇到循环引用的时候报错（建议设置为Error，这样更规范）
                DateFormatString = "yyyy-MM-dd HH:mm:ss", //日期格式化，默认的格式也不好看
                ContractResolver = new CamelCasePropertyNamesContractResolver() //json中属性开头字母小写的驼峰命名
            };
            string json = JsonConvert.SerializeObject(data, settings);
            return await Task.FromResult(json);
        }

        [HttpGet]
        public async Task<ActionResult> ListData2()
        {
            var data = new
            {
                errcode = 0,
                data = list
            };
            return await Task.FromResult(Json(data, JsonRequestBehavior.AllowGet));
        }

        /// <summary>
        /// 控制器,视图之前传值
        /// </summary> 
        //[Route("Test/{id:int}")]
        public async Task<ActionResult> Test(int i)
        {
            //控制器向视图传值：
            ViewData["name"] = "hello";
            ViewBag.Title = "Hello!";

            this.TempData["str"] = "sealee";
            return View();
        }

        public async Task<ActionResult> Test2()
        {
            string str = TempData["str"].ToString();
            return View();
        }

        /// <summary>
        /// 下载文件只能使用get
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<FileResult> Download()
        {
            Stream stream = new MemoryStream();
            return await Task.FromResult(File(stream, "application/vnd.ms-excel", "fileName"));
        }

        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            HttpPostedFileBase file = Request.Files[0];
            if (file.ContentLength == 0)
            {
                return base.Json(new { errcode = 1, errmsg = "未找到文件" });
            }
            else
            {
                file = Request.Files[0];
                return Json(new { errcode = 0, msg = "success" });
            }
        }
    }


    public class Student
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }


    }
}
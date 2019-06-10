using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcEmpty.Areas.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: Web/Test
        public ActionResult Index()
        {
            return View();
        }
    }
}
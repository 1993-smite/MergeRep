using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebVueTest.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult JSTestJasmine()
        {
            return View();
        }

        public IActionResult Basket()
        {
            return View();
        }

        public IActionResult CheckWrite()
        {
            return View();
        }

        public IActionResult Places()
        {
            return View();
        }

        public IActionResult Loudlinks()
        {
            return View();
        }

        public IActionResult FormMask()
        {
            return View();
        }

        public IActionResult Push()
        {
            return View();
        }

        public IActionResult KY()
        {
            return View();
        }

        public IActionResult AjaxTestProgress()
        {
            return View();
        }

        public IActionResult GetData()
        {
            var rer = new List<string>();
            rer.Add("dfgsdfg");
            rer.Add("dfgsdfg");
            rer.Add("dfgsdfg");
            rer.Add("dfgsdfg");

            Thread.Sleep(20000);
            Thread.Sleep(20000);
            Thread.Sleep(20000);

            return Json(rer);
        }
    }
}
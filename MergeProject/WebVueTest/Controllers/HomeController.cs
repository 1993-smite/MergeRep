using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebVueTest.Models;

namespace WebVueTest.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            //return RedirectToAction("Index","Merge");
            return RedirectToAction("Index", "Logo");
            //return RedirectToAction("Index","SVG");
            //return View();
        }

        public IActionResult Html()
        {
            return View();
        }
    }
}

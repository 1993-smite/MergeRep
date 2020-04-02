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
    public class LogoController : Controller
    {
        public LogoController()
        {
        }

        public IActionResult Index()
        {
            return View("Index");
        }
    }
}

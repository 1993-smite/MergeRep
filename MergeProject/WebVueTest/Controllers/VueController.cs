﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebVueTest.Controllers
{
    public class VueController : Controller
    {
        public IActionResult Vuex()
        {
            return View();
        }
        public IActionResult VueRouter()
        {
            return View();
        }

        public IActionResult VueMixView()
        {
            return View();
        }

        public IActionResult VueComponents()
        {
            return View();
        }

        public IActionResult Vue3()
        {
            return View();
        }
    }
}
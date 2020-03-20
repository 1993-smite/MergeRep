using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebVueTest.Models;

namespace WebVueTest.Controllers
{
    public class AppController : Controller
    {
        public string Login =>
            HttpContext.Request.Cookies[appUser.sessionKey];
            //HttpContext.Session.GetString(appUser.sessionKey);

        public AppController()
        {

        }
    }
}

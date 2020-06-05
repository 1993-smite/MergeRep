using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebVueTest.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Erroroller
        public ActionResult Error(Exception exception)
        {
            return View(exception);
        }
    }
}
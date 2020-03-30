using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebVueTest.DB;
using WebVueTest.Models;

namespace WebVueTest.Controllers
{
    public class LoginController : Controller
    {
        private Lazy<appUserMapper> _mapper = new Lazy<appUserMapper>(() => new appUserMapper());

        [HttpGet]
        public IActionResult Index(string url)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(appUser user)
        {
            if (string.IsNullOrEmpty(user.Login))
                return View(user);

            var usr = _mapper.Value.GetAppDBUser(user.Login);
            if (usr == null)
            {
                return View(user);
            }
            var checkUser = usr.CheckPassword(user.PasswordEnter);

            if (checkUser)
            {
                HttpContext.Response.Cookies.Append(appUser.sessionKey, user.Login);
                //HttpContext.Session.SetString(appUser.sessionKey, user.Login);
                return RedirectToAction("Index", "Home");
            }
            else return View(user);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebVueTest.DB;
using WebVueTest.Models;

namespace WebVueTest.Controllers
{
    public class LoginController : Controller
    {
        public const string returnUrlKey = "returnUrl";
        private Lazy<appUserMapper> _mapper = new Lazy<appUserMapper>(() => new appUserMapper());

        public LoginController()
        {
        }

        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            HttpContext.Response.Cookies.Delete(appUser.sessionKey);
            ViewData[returnUrlKey] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user, string returnUrl)
        {
            if (string.IsNullOrEmpty(user.Login))
                return View(user);

            var usr = _mapper.Value.GetAppDBUser(user.Login);
            if (usr == null)
            {
                ViewData[returnUrlKey] = returnUrl;
                return View(user);
            }
            var checkUser = usr.CheckPassword(user.PasswordEnter);

            if (checkUser)
            {
                //_signInManager.SignInAsync(user, false);
                HttpContext.Session.SetString(appUser.sessionKey, user.Login);
                HttpContext.Response.Cookies.Append(appUser.sessionKey, user.Login, new CookieOptions() {
                    Expires = DateTime.Now.AddDays(1)
                });
                //HttpContext.Session.SetString(appUser.sessionKey, user.Login);
                return Redirect(returnUrl);
            }
            else return View(user);
        }
    }
}
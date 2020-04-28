using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using WebVueTest.MiddleWare.Configurations;
using WebVueTest.Models;

namespace WebVueTest.Controllers
{
    
    public class LanguageController : Controller
    {
        public const string UrlViewData = "Language.returnUrl";
        public const string CheckLanguageViewData = "Language.checkLanguage";

        private readonly IConfiguration configuration;

        public LanguageController(IConfiguration config)
        {
            configuration = config;
        }

        public IActionResult Index(string returnUrl = "")
        {
            ViewData[LanguageController.UrlViewData] = returnUrl;
            var checkCulture = Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];
            ViewData[LanguageController.CheckLanguageViewData] = checkCulture;
            var supportedCulturesString = configuration[ConfigurationKyes.CultureInfosSupported];
            var supportedCultures = supportedCulturesString.Split(",").Select(x => new CultureInfo(x)).ToList();
            return View(supportedCultures);
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            HttpContext.Session.SetString(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)));
            //HttpContext.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName]. = CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture));
            HttpContext.Request.Cookies.Append(new KeyValuePair<string, string>(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture))));
            HttpContext.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(1)
            });
            if (string.IsNullOrEmpty(returnUrl))
            {
                return Content("OK");
            }
            else return LocalRedirect(returnUrl);
        }
    }
}

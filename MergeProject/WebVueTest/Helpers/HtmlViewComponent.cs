using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebVueTest.Models;

namespace ViewComponentsApp.Components
{
    [ViewComponent]
    public class User: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            string login = HttpContext.Request.Cookies[appUser.sessionKey] ?? "";
            return new ContentViewComponentResult(login); ;
        }
    }
}

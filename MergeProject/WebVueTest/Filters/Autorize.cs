using System;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVueTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace WebVueTest.Filters
{
    public class AutorizeAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string login;
            byte[] user;
            if (!context.HttpContext.Session.TryGetValue(appUser.sessionKey,out user))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    {"controller", "Login"},
                    {"action",  "Index"},
                    {"returnUrl", context.HttpContext.Request.Path}
                });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
                
        }
    }
}

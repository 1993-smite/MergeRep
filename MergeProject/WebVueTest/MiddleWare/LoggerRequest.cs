using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebVueTest.MiddleWare
{
    /// <summary>
    /// middle ware for loggin for request 
    /// </summary>
    public class LoggerRequest
    {
        private readonly RequestDelegate _next;

        public LoggerRequest(RequestDelegate next)
        {
            this._next = next;
        }

        private void AddRequest(string request)
        {
            using (StreamWriter sw = new StreamWriter("logRequest.log", true))
            {
                sw.WriteLine(request);
            }
        }

        public async Task InvokeAsync(HttpContext context)
        {
            this.AddRequest($"{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} -:- {context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}");

            await _next.Invoke(context);
        }
    }
}

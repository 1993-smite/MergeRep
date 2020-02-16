using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ViewMerge.Models;

namespace ViewMerge.Controllers
{
    public class AppController : Controller
    {
        private readonly IStringLocalizer<AppController> _localizer;
        public AppController(IStringLocalizer<AppController> localizer)
        {
            _localizer = localizer;
        }
    }
}

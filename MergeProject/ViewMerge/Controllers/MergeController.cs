﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ViewMerge.Models;

namespace ViewMerge.Controllers
{
    public class MergeController : Controller
    {
        List<UserView> list = new List<UserView>();

        private void GetData()
        {
            for(int i = 1; i < 10; i++)
            {
                list.Add(FactoryUserView.Create(i));
            }
        }

        public IActionResult Index()
        {
            GetData();
            return View(list);
        }

        public IActionResult Card(int i = 5)
        {
            GetData();
            var model = list.FirstOrDefault(x => x.Id == i);
            return View(model);
        }

        [HttpPost]
        public IActionResult Card(UserView mdl)
        {
            return View(mdl);
        }
    }
}
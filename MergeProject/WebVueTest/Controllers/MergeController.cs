using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebVueTest.Models;

namespace WebVueTest.Controllers
{
    public class MergeController : AppController
    {
        string fileManager = @"D:\Files";
        public MergeController(): base()
        {

        }

        List<UserViewValidate> list = new List<UserViewValidate>();

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
        public IActionResult Card(UserViewValidate mdl)
        {
            return View(mdl);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(int Id,IFormFile uploadedFile)
        {
            var files = HttpContext.Request;
            if (uploadedFile != null)
            {
                string path = $"{fileManager}\\{Id}";
                string fname = FileManager.GenerateFileName(path, uploadedFile.FileName);
                // путь к папке Files
                path = $"{fileManager}\\{Id}\\{fname}";
                // сохраняем файл в папку Files в каталоге wwwroot


                using (var fileStream = new FileStream(path, FileMode.Append))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

            }

            return Content("OK");
        }
    }
}
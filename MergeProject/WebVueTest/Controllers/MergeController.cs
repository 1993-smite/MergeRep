using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebVueTest.Models;

namespace WebVueTest.Controllers
{
    public class MergeController : AppController
    {
        private IHostingEnvironment _he;
        private readonly string fileManager;//;= IServer.MapPath("~/images/Users");//@"D:\Files";
        private readonly string fileDir = "images\\Users";
        private string fileHostManager;
        public MergeController(IHostingEnvironment he): base()
        {
            _he = he;
            //var request = HttpContext.Request;
            fileManager = $"{_he.WebRootPath}\\{fileDir}";
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

        public IActionResult Card(int id = 5)
        {
            GetData();
            var model = list.FirstOrDefault(x => x.Id == id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Card(UserViewValidate mdl)
        {
            int lastId = mdl.Id;
            int id = FactoryUserView.SaveUserView((UserView)mdl);

            foreach(var file in mdl.Files)
            {
                string path = $"{fileManager}\\{lastId}";
                string targetPath = $"{fileManager}\\{id}";
                if (id != lastId)
                {
                    FileManager.MoveFile(path, targetPath, file.FileName);
                    var request = HttpContext.Request;
                    var uriBuilder = new UriBuilder
                    {
                        Host = request.Host.Host,
                        Scheme = request.Scheme,
                        Port = request.Host.Port.HasValue ? request.Host.Port.Value : -1,
                        Path = fileDir
                    };

                    file.FullFileName = $"{uriBuilder.Uri.LocalPath}/{id}/{file.FileName}";
                }
            }

            return View(mdl);
        }

        [HttpPost]
        public IActionResult AddFile(int Id,IFormFile uploadedFile)
        {
            var request = HttpContext.Request;
            UserFile file = new UserFile
            {
                UserId = Id,
                FileName = uploadedFile.FileName,
                ContentType = uploadedFile.ContentType
            };
            var files = HttpContext.Request;
            if (uploadedFile != null)
            {
                string path = $"{fileManager}\\{Id}";
                string fname = FileManager.GenerateFileName(path, uploadedFile.FileName);
                // путь к папке Files
                path = $"{fileManager}\\{Id}\\{fname}";
                // сохраняем файл в папку Files в каталоге wwwroot

                var uriBuilder = new UriBuilder
                {
                    Host = request.Host.Host,
                    Scheme = request.Scheme,
                    Port = request.Host.Port.HasValue ? request.Host.Port.Value : -1,
                    Path = fileDir
                };

                file.FullFileName = $"{uriBuilder.Uri.LocalPath}/{Id}/{fname}";
                using (var fileStream = new FileStream(path, FileMode.Append))
                {
                    uploadedFile.CopyTo(fileStream);
                }

            }

            return View("UserFile", file);
        }
    }
}
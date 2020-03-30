using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using WebVueTest.DB;
using WebVueTest.DB.Mappers;
using WebVueTest.Models;

namespace WebVueTest.Controllers
{
    public class MergeController : AppController
    {
        private IHostingEnvironment _he;
        private readonly string fileManager;//;= IServer.MapPath("~/images/Users");//@"D:\Files";
        private readonly string fileDir = "images\\Users";
        private string fileHostManager;
        private Lazy<ChatHub> chat = new Lazy<ChatHub>(() => new ChatHub());

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
            var context = HttpContext.Response;
            GetData();
            return View(list);
        }

        public IActionResult ChangeUser(string login)
        {
            HttpContext.Response.Cookies.Append(appUser.sessionKey, login);
            return Content("Ok");
        }

        [HttpPost]
        public IActionResult SaveUserComment(MergeUserComment comment)
        {
            comment.CreatedUser = UserMapper.GetUser(Login);
            UserCommentFactory.SaveUserComment(comment);
            chat.Value.SendToGroup(comment);
            return Content("Ok");
        }

        public IActionResult Card(int id = 5)
        {
            string user = Login;
                //HttpContext.Session.GetString(appUser.sessionKey);

            if (string.IsNullOrEmpty(user))
            {
                return RedirectToActionPermanent("Index", "Login");
            }

            ViewData[appUser.sessionKey] = user;

            //GetData();
            var model = FactoryUserView.Convert<User,UserViewValidate>(FactoryUserView.GetUser(id));//list.FirstOrDefault(x => x.Id == id);

            ViewData["Users"] = FactoryUserView.CreateUsers(id);

            var mapper = new Mapper(MergeUserComment.config);

            ViewData["Comments"] = UserCommentFactory.GetUserComments(id);
                /*UserCommentFactory
                .CreateCommnets((List<User>)ViewData["Users"], 20)
                                .Select(x=> UserCommentFactory.ConvertComment(x,model.Id));*/
            return View(model);
        }

        [HttpPost]
        public IActionResult Card(UserViewValidate mdl)
        {
            int lastId = mdl.Id;
            int id = 0;//FactoryUserView.SaveUserView((UserView)mdl);

            if (mdl.Files != null)
            {
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
            }

            id = FactoryUserView.SaveUser(mdl);

            return RedirectToActionPermanent("Card", "Merge", new { Id = id });
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
                var fdata = fname.Split(".");
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
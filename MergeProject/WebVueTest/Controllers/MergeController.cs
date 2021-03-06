﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DB.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using WebVueTest.Controllers.api;
using WebVueTest.DB;
using WebVueTest.DB.Mappers;
using WebVueTest.Filters;
using WebVueTest.Models;

namespace WebVueTest.Controllers
{
    public enum SaveCommentType
    {
        New,
        Invoit
    }

    [Autorize]
    public class MergeController : AppController
    {
        public static class MergeUserHub
        {
            public const string TemplateGroupCard = "Merge[{0}].Card";
            public const string TemplateGroupCommon = "Merge";
            public const string ChangeModel = "ChangeModel";
            public const string SaveComment = "SaveComment";
        }

        private IHostingEnvironment _he;
        private readonly string fileManager;//;= IServer.MapPath("~/images/Users");//@"D:\Files";
        private readonly string fileDir = "images\\Users";
        private string fileHostManager;
        private UserController userController;

        private readonly IHubContext<CommonHub> _hubContext;
        private async Task SendMessage(string groupName, string message)
        {
            var group = _hubContext.Clients.Group(groupName);
            await _hubContext.Clients.Group(groupName).SendAsync("ChangeModel",message);
        }

        public MergeController(IHostingEnvironment he, IHubContext<CommonHub> hubContext) : base()
        {
            _hubContext = hubContext;
            _he = he;
            //var request = HttpContext.Request;
            userController = new UserController();
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

        public string GetMessage(int userId)
        {
            var user = FactoryUserView.GetUser(userId);
            return user.City;
        }

        public IActionResult Index()
        {
            var context = HttpContext.Response;
            return View();
        }

        public IActionResult ChangeUser(string login)
        {
            HttpContext.Response.Cookies.Append(appUser.sessionKey, login);
            return Content("Ok");
        }

        [HttpPost]
        public MergeUserComment SaveUserComment(MergeUserComment comment, SaveCommentType type)
        {
            switch (type)
            {
                case SaveCommentType.New:
                    comment.CreatedUser = UserMapper.GetUser(Login);
                    comment.UpdateDt = comment.CreateDt;
                    comment.Id = UserCommentFactory.SaveUserComment(comment);
                    break;
                case SaveCommentType.Invoit:
                    UserRepository.SaveUserCommentInvoit(new PostgresApp.DBUserCommentInvoit()
                    {
                        UserCommentId = comment.Id,
                        UserId = comment.UserId
                    });
                    break;
                default:
                    throw new ArgumentException("Invalid argument 'type'");
                    break;
            }
            comment.CardId = comment.UserId;
            return comment;
        }

        public IActionResult Card(int id = 0)
        {
            string user = Login;

            ViewData[appUser.sessionKey] = user;

            //GetData();
            var model = id < 1 
                ? new UserViewValidate() 
                : userController.Get(id);
            //list.FirstOrDefault(x => x.Id == id);

            //ViewData["Users"] = FactoryUserView.CreateUsers(id);

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

            //id = FactoryUserView.SaveUser(mdl);

            string message = mdl.Id < 1 ? "Добавился" : "Изменился";
            message = $"{message} юзер \"{mdl.FullName}\"";
            SendMessage(MergeUserHub.TemplateGroupCommon, message);

            if (mdl.Id > 0)
                SendMessage(string.Format(MergeUserHub.TemplateGroupCard, id), message);

            return RedirectToActionPermanent("Index", "Merge");
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
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebVueTest.Models;

namespace WebVueTest.Controllers
{
    public class FileSystemInfoConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(FileSystemInfo).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var jObject = JObject.Load(reader);
            var name = jObject["Name"].Value<string>();
            var lastWriteTime = jObject["LastWriteTime"].Value<string>();
            var creationTime = jObject["CreationTime"].Value<string>();
            var extension = jObject["Extension"].Value<string>();
            var fullName = jObject["FullName"].Value<string>();
            return Activator.CreateInstance(objectType, name, lastWriteTime
                , creationTime, extension, fullName);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var info = value as FileSystemInfo;
            var obj = info == null
                ? null
                : new
                {
                    name = info.Name,
                    lastWriteTime = info.LastWriteTime,
                    lastWriteTimeStr = info.LastWriteTime.DateTimeRusAppFormat(),
                    creationTime = info.CreationTime,
                    creationTimeStr = info.CreationTime.DateTimeRusAppFormat(),
                    extension = info.Extension,
                    fullName = info.FullName,

                };
            var token = JToken.FromObject(obj);
            token.WriteTo(writer);
        }
    }

    public class FilesController : AppController
    {
        public static class ViewDataKyes
        {
            public static readonly string pathRelativeDirectory = "pathRelativeDirectory";
        } 

        private IHostingEnvironment _he;
        private readonly string serverDirectory;
        private readonly string pathToSystemDirectory;

        public FilesController(IHostingEnvironment he):base()
        {
            _he = he;
            serverDirectory = "Files";
            pathToSystemDirectory = $"{_he.WebRootPath}\\{serverDirectory}";
        }

        public IActionResult Index(string path = null)
        {
            ViewData[ViewDataKyes.pathRelativeDirectory] = path != null 
                ? $"{serverDirectory}/{Path.GetRelativePath(pathToSystemDirectory, path).Replace("\\", "/")}" 
                : serverDirectory;
            path = path ?? pathToSystemDirectory;
            DirectoryView directory = new DirectoryView(path);
            directory.Files = FileManager.GetFiles(path).ToList();
            directory.Directories = Directory.GetDirectories(path).Select(x=>new DirectoryAppInfo(path,new DirectoryInfo(x))).ToList();

            return View(directory);
        }
    }
}
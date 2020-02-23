using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebVueTest.Models
{
    public static class FileManager
    {
        private static bool checkFile(string path, string fileName)
        {
            string fullName = $"{path}\\{fileName}";
            return File.Exists(fullName);
        }

        public static string GenerateFileName(string path, string fileName, string type = null)
        {
            Directory.CreateDirectory(path);
            if (string.IsNullOrEmpty(fileName.Trim()))
            {
                throw new ArgumentException("Не задан параметр", nameof(fileName));
            }
            if (string.IsNullOrEmpty(type))
            {
                var fileAttr = fileName.Split('.');
                fileName = fileAttr[0];
                type = fileAttr[1] ?? "";
            }
            string fname = $"{fileName}.{type}";
            int index = 1;
            while (checkFile(path, fname))
            {
                fname = $"{fileName} ({index++}).{type}";
            }
            return fname;
        }
    }
}

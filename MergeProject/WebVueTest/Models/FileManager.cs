using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebVueTest.Models
{
    public static class FileManager
    {

        private static string getFullFileName(string path, string fileName)
        {
            return $"{path}\\{fileName}";
        }

        private static bool checkFile(string path, string fileName)
        {
            string fullName = getFullFileName(path,fileName);
            return File.Exists(fullName);
        }

        public static bool MoveFile(string path, string newPath, string fileName)
        {
            if (!checkFile(path, fileName))
                throw new ArgumentException($"File {fileName} not found by path {path}");
            if (checkFile(newPath, fileName))
            {
                throw new ArgumentException($"File {fileName} already exist by path {newPath}");
            }
            File.Move(getFullFileName(path, fileName), getFullFileName(newPath,fileName));
            return checkFile(newPath, fileName);
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
                type = fileAttr.LastOrDefault() ?? "";
                fileName = string.Join('.',fileAttr.Take(fileAttr.Length - 2)).Trim();
                fileName = string.IsNullOrEmpty(fileName) ? fileAttr[0] : fileName;
            }
            string fname = $"{fileName}.{type}";
            int index = 1;
            while (checkFile(path, fname))
            {
                fname = $"{fileName} ({index++}).{type}";
            }
            return fname;
        }

        /// <summary>
        /// получение файлов по директории
        /// </summary>
        /// <param name="pathToDirectory"></param>
        /// <returns></returns>
        public static IEnumerable<FileSystemInfo> GetFiles(string pathToDirectory)
        {
            var pathFiles = Directory.GetFiles(pathToDirectory);
            var files = new List<FileSystemInfo>();
            foreach (var path in pathFiles)
            {
                files.Add(new FileInfo(path));
            }
            return files;
        }
    }
}

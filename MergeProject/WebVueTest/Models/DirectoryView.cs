using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebVueTest.Models
{
    public class DirectoryAppInfo
    {
        public string Name { get; private set; }
        public string Path => $"{ParentPath}\\{Name}";
        public string ParentPath { get; private set; }
        public DateTime LastUpdate { get; private set; }
        public string LastUpdateStr => LastUpdate.DateTimeRusAppFormat();
        public DateTime CreateDt { get; private set; }
        public string CreateDtStr => CreateDt.DateTimeRusAppFormat();

        public DirectoryAppInfo(string parentPath, DirectoryInfo directoryInfo)
        {
            ParentPath = parentPath;
            Name = directoryInfo.Name;
            LastUpdate = directoryInfo.LastWriteTime;
            CreateDt = directoryInfo.CreationTime;
        }

        public DirectoryAppInfo(string parentPath, string name)
        {
            Name = name;
            ParentPath = parentPath;
        }
    }

    public class DirectoryView
    {
        public DirectoryAppInfo directoryAppInfo { get; }
        public List<DirectoryAppInfo> Directories { get; set; }
        public List<FileSystemInfo> Files { get; set; }

        public DirectoryView()
        {

        }

        public DirectoryView(DirectoryInfo directoryInfo): base()
        {

        }

        public DirectoryView(string path) : base()
        {
            var dirInfo = new DirectoryInfo(path);
            directoryAppInfo = new DirectoryAppInfo(dirInfo.Parent.FullName, dirInfo);
        }

        public DirectoryView(string path, string name):base()
        {
            directoryAppInfo = new DirectoryAppInfo(path, name);
        }
    }
}

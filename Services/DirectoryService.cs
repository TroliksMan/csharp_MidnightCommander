using MidnightCommander.Components.PopUp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidnightCommander.Services
{
    public class DirectoryService
    {
        public List<DirectoryInfo> GetDirectory(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DirectoryInfo[] dirs = dirInfo.GetDirectories();

            return dirs.ToList<DirectoryInfo>();
        }
        public List<FileInfo> GetFiles(string path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] files = dirInfo.GetFiles();

            return files.ToList<FileInfo>();
        }
        public string GetUpperDir(string path)
        {
            string result = path;
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Parent != null)
                result = dir.Parent.FullName;
            return result;
        }
        public static string Space(string path)
        {
            StringBuilder sb = new StringBuilder();
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            DriveInfo driveInfo = new DriveInfo(dirInfo.Root.Name); ;

            string freeSpace = (driveInfo.TotalFreeSpace / 1073741824).ToString();
            string totalSize = (driveInfo.TotalSize / 1073741824).ToString();

            int percentInt = Convert.ToInt32((Convert.ToDouble(freeSpace) / Convert.ToDouble(totalSize)) * 100);
            string percent = percentInt.ToString();
            sb.Append($" {freeSpace}G / {totalSize}G ({percent}%) ");
            return sb.ToString();
        }
        public static List<string> GetDrives()
        {
            List<String> result = new List<string>();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo item in drives)
            {
                if (item.IsReady)
                    result.Add(item.Name);
            }

            return result;
        }
    }
}

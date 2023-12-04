using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace NowPlayingMonitor_WPF.Util
{
    public class PathUtil
    {

        public static string? GetCurrentDirectory()
        {
            string executablePath = Assembly.GetExecutingAssembly().Location;
            return Path.GetDirectoryName(executablePath);
        }

        public static string GetPortalWorkDirectory()
        {
            string executablePath = Assembly.GetExecutingAssembly().Location;
            string? directoryPath = Path.GetDirectoryName(executablePath);
            if (directoryPath == null) return "";

            string workSpacePath = Path.Combine(directoryPath, "Work Space");
            if (!Directory.Exists(workSpacePath))
            {
                Directory.CreateDirectory(workSpacePath);
            }

            return workSpacePath;
        }

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void DeleteDirectory(string path)
        {
           if (Directory.Exists(path))
                Directory.Delete(path, true);
        }

        public static bool IsParentDirectoryExists(string filePath)
        {
            DirectoryInfo? parentDirectory = Directory.GetParent(filePath);
            return parentDirectory != null && parentDirectory.Exists;
        }

        public static void CreateParentDirectory(string filePath)
        {
            string? directoryPath = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        

    }
}

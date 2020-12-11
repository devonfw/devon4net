using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Devon4Net.Infrastructure.Common
{
    public static class FileOperations
    {
        private static string ApplicationPath {get;set;}

        public static List<string> GetFilesFromPath(string searchPattern, string defaultDirectory = null)
        {
            var workingDirectory = string.IsNullOrEmpty(defaultDirectory) || !Directory.Exists(defaultDirectory) ? Directory.GetCurrentDirectory() : defaultDirectory;
            return Directory.GetFiles(workingDirectory, searchPattern, SearchOption.AllDirectories).ToList();
        }


        public static string GetFileFullPath(string fileName, string defaultDirectory = null)
        {
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            if (File.Exists(fileName)) return Path.GetFullPath(fileName);

            var workingDirectory = string.IsNullOrEmpty(defaultDirectory) || !Directory.Exists(defaultDirectory) ? Directory.GetCurrentDirectory() : defaultDirectory;

            var theFile = Directory.GetFiles(workingDirectory, fileName, SearchOption.AllDirectories).FirstOrDefault();
            return theFile;
        }

        public static string GetApplicationPath()
        {
            if (string.IsNullOrEmpty(ApplicationPath))
            { 
                ApplicationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }
            return ApplicationPath;
        }
    }
}
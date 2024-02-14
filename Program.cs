using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineCounter
{
    class Program
    {
        private static List<FileData> fileList = new List<FileData>();
        private static List<FileSystemInfo> dirList = new List<FileSystemInfo>();
        private static int TotalLines = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter full directory path and press enter");
            string dirPath = Console.ReadLine();
            //string dirPath = @"C:\\i-free\\Sunlight\\trunk";

            if (Directory.Exists(dirPath))
            {
                DirectoryInfo di = new DirectoryInfo(dirPath);
                ListDirectoriesAndFiles(di.GetFileSystemInfos());

                foreach (var file in fileList)
                {
                    if (file.FileItem.Extension == ".cs")
                    {
                        file.Count = FileReader.ReadFile(file.FileItem.FullName);
                    }
                }

                Console.WriteLine("Dirs: {0} Files: {1}", dirList.Count, fileList.Count);

                foreach (var file in fileList)
                {
                    if (file.Count > 0)
                    {
                        TotalLines += file.Count;
                    }
                }

                
                Console.WriteLine("Total lines: {0}", TotalLines);
            }
            else
            {
                Console.WriteLine("Directory doesn't exist");
            }
            Console.WriteLine("Directory: {0}", dirPath);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void ListDirectoriesAndFiles(FileSystemInfo[] fsiArr)
        {
            foreach (FileSystemInfo fsi in fsiArr)
            {
                FileAttributes attr = File.GetAttributes(fsi.FullName);

                if (attr.HasFlag(FileAttributes.Directory))
                {
                    dirList.Add(fsi);
                    DirectoryInfo di = new DirectoryInfo(fsi.FullName);
                    ListDirectoriesAndFiles(di.GetFileSystemInfos());
                }
                else
                {
                    fileList.Add(new FileData() { FileItem = fsi });
                }
            }
        }
    }
}

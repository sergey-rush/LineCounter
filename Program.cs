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
        
        static void Main(string[] args)
        {
            while (true)
            {
                fileList.Clear();
                dirList.Clear();
                int totalLines = 0;
                Console.Clear();
                Console.WriteLine("Please enter full directory path and press enter");
                Console.WriteLine("Use SHIFT+INSERT to paste the directory path");
                string dirPath = Console.ReadLine();

                if (Directory.Exists(dirPath))
                {
                    Console.WriteLine("Working... Please wait");
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
                            totalLines += file.Count;
                        }
                    }

                    Console.WriteLine("-----");

                    Console.WriteLine("Total lines: {0}", totalLines);
                }
                else
                {
                    Console.WriteLine("----- Directory doesn't exist -----");
                }

                Console.WriteLine("Directory: {0}", dirPath);
                Console.WriteLine("Press any key to start again");
                Console.ReadKey();
            }
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

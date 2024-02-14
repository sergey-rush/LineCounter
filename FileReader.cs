using System.Collections.Generic;
using System.IO;

namespace LineCounter
{
    public class FileReader
    {
        public static int ReadFile(string path)
        {
            List<string> lines = new List<string>();

            string line;

            using (StreamReader file = new StreamReader(path))
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length > 1)
                    {
                        lines.Add(line);
                    }
                }
            }

            return lines.Count;
        }
    }
}
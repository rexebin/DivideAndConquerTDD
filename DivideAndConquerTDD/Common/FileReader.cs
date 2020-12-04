﻿using System.IO;

namespace DivideAndConquerTDD.Common
{
    public class FileReader
    {
        public string GetPath(string folderName, string fileName)
        {
            return Path.GetFullPath(@$"..\..\..\{folderName}\{fileName}");
        }

        public string[] ReadFile(string folderName, string fileName)
        {
            return File.ReadAllLines(GetPath(folderName, fileName));
        }
    }
}
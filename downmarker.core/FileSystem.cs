using System;
using System.IO;
using System.Text;

namespace DownMarker.Core
{
    public class FileSystem : IFileSystem
    {
        public string ReadTextFile(string absoluteFilePath)
        {
            if (!Path.IsPathRooted(absoluteFilePath))
                throw new ArgumentException("Not an absolute file path");
            return File.ReadAllText(absoluteFilePath, Encoding.UTF8);
        }

        public void WriteTextFile(string absoluteFilePath, string content)
        {
            if (!Path.IsPathRooted(absoluteFilePath))
                throw new ArgumentException("Not an absolute file path");
            File.WriteAllText(absoluteFilePath, content);
        }

        public string GetAbsoluteFilePath(string absoluteOrRelativeFilePath)
        {
            return Path.GetFullPath(absoluteOrRelativeFilePath);
        }

        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public void Delete(string absoluteFilePath)
        {
            File.Delete(absoluteFilePath);
        }
    }
}

namespace DownMarker.Core
{
    public interface IFileSystem
    {
        string GetAbsoluteFilePath(string absoluteOrRelativeFilePath);
        bool Exists(string path);
        void Delete(string path);
        string ReadTextFile(string absoluteFilePath);
        void WriteTextFile(string absoluteFilePath, string content);
    }
}

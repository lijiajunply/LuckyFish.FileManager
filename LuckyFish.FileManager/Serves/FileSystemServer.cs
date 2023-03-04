using System.IO;

namespace LuckyFish.FileManager.Serves;

public static class FileSystemServer
{
    /// <summary>
    /// Copy
    /// </summary>
    /// <param name="path">Path</param>
    /// <param name="newPath">is Directory Path</param>
    public static void Copy(string path, string newPath)
    {
        if (IsDir(path))
        {
            var dir = new DirectoryInfo(path);
            Directory.CreateDirectory(newPath + "\\"+dir.Name);
            foreach (var info in dir.GetFileSystemInfos())
                Copy(info.FullName,newPath + "\\"+dir.Name);
        }
        else
        {
            var file = new FileInfo(path);
            File.Copy(path,newPath+"\\"+file.Name);
        }
    }

    /// <summary>
    /// Cut
    /// </summary>
    /// <param name="path">Path</param>
    /// <param name="newPath">is Directory Path</param>
    public static void Cut(string path, string newPath)
    {
        
    }

    public static void Rename(string path, string newName)
    {
        
    }

    public static void Delete(string path)
    {
        
    }
    public static bool IsDir(string filepath)
    {
        if (Directory.Exists(filepath))
        {
            return true;
        }

        return false;
    }
}
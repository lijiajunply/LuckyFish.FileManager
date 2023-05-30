using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LuckyFish.FileManager.Models;

public static class FileServer
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
            Directory.CreateDirectory(newPath + "\\" + dir.Name);
            foreach (var info in dir.GetFileSystemInfos())
                Copy(info.FullName, newPath + "\\" + dir.Name);
        }
        else
        {
            var file = new FileInfo(path);
            File.Copy(path, newPath + "\\" + file.Name);
        }
    }

    /// <summary>
    /// Cut
    /// </summary>
    /// <param name="path">Path</param>
    /// <param name="newPath">is Directory Path</param>
    public static void Cut(string path, string newPath)
    {
        if (IsDir(path))
            Directory.Move(path, newPath);
        else
            File.Move(path, newPath);
    }

    /// <summary>
    /// Rename
    /// </summary>
    /// <param name="path">Path</param>
    /// <param name="newName">NewName</param>
    public static void Rename(string path, string newName)
    {
        var dic = Path.GetDirectoryName(path);
        if (IsFull(dic + "\\" + newName)) return;
        var file = FromPath(path);
        var extension = file is FileOperation ? file.Extension : "";
        var newPath = dic + "\\" + newName + extension;
        Cut(path, newPath);
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="path">DeletePath</param>
    public static void Delete(string path)
    {
        FromPath(path).Delete();
    }

    public static long GetSize(string path)
    {
        try
        {
            return FromPath(path).Size;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public static bool IsDir(string filepath) => Directory.Exists(filepath) && !File.Exists(filepath);

    private static bool IsFull(string filePath) =>
        Directory.Exists(filePath) || File.Exists(filePath);

    private static IFileSystem FromPath(string path)
    {
        if (IsDir(path))
            return new DirectoryOperation(path);
        return new FileOperation(path);
    }

    public static DirectoryOperation[] GetCommonPath()
        => new DirectoryOperation[]
        {
            new (Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)),
            new (Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)),
            new (Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)),
            new (Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)),
            new (Environment.GetFolderPath(Environment.SpecialFolder.MyPictures))
        };

    public static IFileSystem? Create(string directoryPath,string name,bool isFile)
    {
        var path = Path.Combine(directoryPath,name);
        if (!IsFull(path))
        {
            if (isFile)
                File.Create(path);
            else
                Directory.CreateDirectory(path);
        }
        else
            return null;
        return isFile?new FileOperation(path):new DirectoryOperation(path);
    }

    public static IFileSystem? Get(string path)
    {
        IFileSystem? a = null;
        if (Directory.Exists(path))
            a = new DirectoryOperation(path);
        if (File.Exists(path))
            a = new FileOperation(path);
        return a;
    }

    public static List<DriveSimpleOperation> GetDrives()
        => DriveInfo.GetDrives().Select(x => new DriveSimpleOperation(x.Name)).ToList();
}
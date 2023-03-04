using System;
using System.IO;
using System.Linq;
using Avalonia.OpenGL.Egl;

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
        if (IsDir(path))
            Directory.Move(path,newPath);
        else
            File.Move(path,newPath);
    }
    /// <summary>
    /// Rename
    /// </summary>
    /// <param name="path">Path</param>
    /// <param name="newName">NewName</param>
    public static void Rename(string path, string newName)
    {
        if(IsFull(newName))return;
        var file = FromPath(path);
        var dic = file switch
        {
            DirectoryInfo d => d.Parent.FullName,
            FileInfo f => f.DirectoryName,
            _ => ""
        };
        var extension = file is FileInfo ? file.Extension : "";
        var newPath = dic + "\\" + newName + extension;
        Cut(path,newPath);
    }
    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="path">DeletePath</param>
    public static void Delete(string path)
    {
        if (IsDir(path))
            Directory.Delete(path,true);
        else
            File.Delete(path);
    }
    public static long GetSize(string path)
    {
        try
        {
            if (IsDir(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                long size = 0;
                foreach (var info in dir.GetFileSystemInfos())
                {
                    if (size >= 50000000)
                        return 50000000;
                    size += GetSize(info.FullName);
                }
                return size;
            }
            else
            {
                var file = new FileInfo(path);
                return file.Length;
            }
        }
        catch (Exception e)
        {
            return 0;
        }
    }
    public static bool IsDir(string filepath) => Directory.Exists(filepath);
    public static bool IsFull(string filePath) => 
        Directory.Exists(filePath) || File.Exists(filePath);

    public static FileSystemInfo FromPath(string path)
    {
        if (IsDir(path))
            return new DirectoryInfo(path);
        return new FileInfo(path);
    }
}
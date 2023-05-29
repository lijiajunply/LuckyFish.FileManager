using System;
using System.IO;
using System.Linq;

namespace LuckyFish.FileManager.Models;

public class DirectoryOperation : IFileSystem
{
    public string? ImagePath { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime WriteTime { get; set; }
    public long Size { get; set; }

    public DirectoryOperation(string path)
    {
        Path = path;
        if (!Exist())
            throw new Exception("IFileSystem Error : Is Not Have The Directory");
        var info = new DirectoryInfo(Path);
        Name = info.Name;
        Extension = "";
        CreateTime = info.CreationTime;
        WriteTime = info.LastWriteTime;
        Size = GetSize();
    }

    public IFileSystem[] GetFileSystems()
    {
        var info = new DirectoryInfo(Path).GetFileSystemInfos();
        return info.Select(systemInfo => (IFileSystem)(systemInfo is FileInfo ? new FileOperation(systemInfo.FullName) : new DirectoryOperation(systemInfo.FullName))).ToArray();
    }
    public long GetSize()
    {
        try
        {
            return GetFileSystems().Sum(info => info.Size);
        }
        catch (Exception e)
        {
            return 0;
        }
    }
    public void Rename(string newName)
    {
        var parent = System.IO.Path.GetDirectoryName(Path);
        Move(parent+"\\"+newName);
    }

    public void Delete() => Directory.Delete(Path);

    public void Move(string newDirectoryPath) => Directory.Move(Path, newDirectoryPath);
    public void Copy(string newDirectoryPath)
    {
        Directory.CreateDirectory(newDirectoryPath + "\\" + Name);
        foreach (var info in GetFileSystems())
            info.Copy(newDirectoryPath + "\\" + Name);
    }

    public bool Exist() => Directory.Exists(Path);
}
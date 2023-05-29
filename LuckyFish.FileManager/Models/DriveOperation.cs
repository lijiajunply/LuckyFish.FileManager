using System;
using System.IO;

namespace LuckyFish.FileManager.Models;

public class DriveOperation : IFileSystem
{
    public string Name { get; set; }
    public string Path { get; set; }
    public string Type { get; set; }
    public string Extension { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime WriteTime { get; set; }
    public string DriveFormat { get; set; }
    public long Size { get; set; }
    public long TotalSize { get; set; }
    public string DriveType { get; set; }

    public DriveOperation(string path)
    {
        Path = path;
        Name = path;
        Type = "Drive";
        if (!Exist())
            throw new Exception("");
        var info = new DriveInfo(path);
        DriveFormat = info.DriveFormat;
        Size = info.AvailableFreeSpace;
        TotalSize = info.TotalSize;
        DriveType = info.DriveType.ToString();
        Extension = "";
    }
    public void Rename(string newName){}

    public void Delete(){}

    public void Move(string newDirectoryPath) { }

    public void Copy(string newDirectoryPath) {}

    public bool Exist() => Directory.Exists(Path);
}
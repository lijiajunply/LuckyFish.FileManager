using System;
using System.IO;

namespace LuckyFish.FileManager.Models;

public class FileOperation : IFileSystem
{
    public string? ImagePath { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime WriteTime { get; set; }
    public long Size { get; set; }

    public FileOperation(string path)
    {
        Path = path;
        if (!Exist())
            throw new Exception("IFileSystem Error : Is Not Have The File");
        var info = new FileInfo(path);
        CreateTime = info.CreationTime;
        WriteTime = info.LastWriteTime;
        Size = info.Length;
        Extension = info.Extension;
        Name = info.Name;
    }

    public void Rename(string newName)
    {
        var dic = System.IO.Path.GetDirectoryName(Path);
        var newPath = dic + "\\" + newName + Extension;
        File.Move(Path,newPath);
    }

    public void Delete() => File.Delete(Path);

    public void Move(string newDirectoryPath) => File.Move(Path, newDirectoryPath + "\\" + Name);

    public void Copy(string newDirectoryPath) => File.Copy(Path, newDirectoryPath + "\\" + Name);

    public bool Exist() => File.Exists(Path);
    public long GetSize() => Size;
}
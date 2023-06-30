using System.Globalization;

namespace FileManager.Lib.FileModels;

public class DirectoryOperation : IFileSystem
{
    public string? ImagePath { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public string? CreateTime { get; set; }
    public string? WriteTime { get; set; }
    public long Size { get; set; }

    public DirectoryOperation(string path,bool isRunGetSize = false)
    {
        Path = path;
        if (!Exist())
            throw new Exception("IFileSystem Error : Is Not Have The Directory");
        var info = new DirectoryInfo(Path);
        Name = info.Name;
        Extension = "";
        CreateTime = info.CreationTime.ToString(CultureInfo.CurrentCulture);
        WriteTime = info.LastWriteTime.ToString(CultureInfo.CurrentCulture);
        Size = isRunGetSize ? GetSize() : 0;
        ImagePath = System.IO.Path.Combine(CodeServer.CodePath, "Assets", "dir.svg");
    }

    public IFileSystem[] GetFileSystems(bool isHide = false)
    {
        var info = new DirectoryInfo(Path).GetFileSystemInfos();
        return info.Where(x => isHide ||
                               x.Attributes != FileAttributes.Hidden ||
                               x.Attributes == FileAttributes.System ||
                               x.Attributes == FileAttributes.Temporary ||
                               x.Attributes == FileAttributes.Archive).Select(x =>
                (IFileSystem)(x is FileInfo ? new FileOperation(x.FullName) : new DirectoryOperation(x.FullName)))
            .ToArray();
    }
    public long GetSize()
    {
        try
        {
            return GetFileSystems().Sum(info => info.Size);
        }
        catch (Exception)
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
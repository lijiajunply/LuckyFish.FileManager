namespace LuckyFish.FileManager.Models;

public class FileSystem : IFileSystem
{
    public string? ImagePath { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public string? CreateTime { get; set; }
    public string? WriteTime { get; set; }
    public long Size { get; set; }
    public void Rename(string newName)
    {
        
    }

    public void Delete()
    {
        
    }

    public void Move(string newDirectoryPath)
    {
        
    }

    public void Copy(string newDirectoryPath)
    {
        
    }

    public bool Exist()
    {
        return false;
    }

    public long GetSize()
    {
        return 0;
    }
}
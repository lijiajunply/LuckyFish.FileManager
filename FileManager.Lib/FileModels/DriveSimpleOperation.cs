namespace FileManager.Lib.FileModels;

public class DriveSimpleOperation
{
    public string? ImagePath { get; set; }
    public string Name { get; set; }
    public long TotalSize { get; set; }
    public long Size { get; set; }

    public DriveSimpleOperation(string name)
    {
        Name = name;
        var info = new DriveInfo(name);
        Size = info.AvailableFreeSpace;
        TotalSize = info.TotalSize;
        ImagePath = Path.Combine(CodeServer.CodePath, "Assets", "drive.svg" );
    }
}
using LuckyFish.FileManager.Serves;

namespace LuckyFish.FileManager.Models;

public class CommonModel
{
    public string? ImagePath { get; set; }
    public string Path { get; set; }
    public string Name { get; set; }

    public CommonModel(string path)
    {
        Path = path;
        Name = System.IO.Path.GetFileNameWithoutExtension(path);
        ImagePath = System.IO.Path.Combine(CodeServer.CodePath, "Assets", "dir.svg");
    }
}
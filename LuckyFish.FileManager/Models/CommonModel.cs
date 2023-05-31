using System.IO;
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
        var a = System.IO.Path.Combine(CodeServer.CodePath, "Assets","Common",$"{Name}.svg");
        ImagePath = a;
        if (File.Exists(a))
        {
            ImagePath = a;
        }
        else
        {
            if (File.Exists(path))
            {
                ImagePath = System.IO.Path.Combine(CodeServer.CodePath, "Assets","file.svg");
            }
            if (Directory.Exists(path))
            {
                ImagePath = System.IO.Path.Combine(CodeServer.CodePath, "Assets","dir.svg");
            }
        }
    }
}
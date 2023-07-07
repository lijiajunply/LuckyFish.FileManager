using System.IO;
using FileManager.Lib;
using LuckyFish.FileManager.Serves;

namespace LuckyFish.FileManager.Models;

public class CommonModel
{
    public string Path { get; set; }
    public string Name { get; set; }

    public CommonModel(string path)
    {
        Path = path;
        Name = System.IO.Path.GetFileNameWithoutExtension(path);
    }
}
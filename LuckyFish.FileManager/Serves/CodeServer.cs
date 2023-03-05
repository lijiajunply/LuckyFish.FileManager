using System;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using LuckyFish.FileManager.Models;

namespace LuckyFish.FileManager.Serves;

public static class CodeServer
{
    public static string CodePath
    {
        get
        {
            var directory = AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);
            var slice = new ArraySegment<string>(directory, 0, directory.Length - 4);
            return Path.Combine(slice.ToArray());
        }
    }
    public static IImage DirImage => new Bitmap(CodePath + "\\Assets\\dir.jpg");
    public static IImage FileImage => new Bitmap(CodePath + "\\Assets\\file.jpg");
    public static IImage DriveImage => new Bitmap(CodePath + "\\Assets\\drive.jpg");
    public static string Root => Path.GetPathRoot(CodePath);
}
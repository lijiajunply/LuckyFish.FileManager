using System;
using System.IO;

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
}
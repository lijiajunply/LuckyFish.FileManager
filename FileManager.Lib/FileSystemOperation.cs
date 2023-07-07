namespace FileManager.Lib;

public static class FileSystemOperation
{
    /// <summary>
    /// Cut
    /// </summary>
    /// <param name="path">Path</param>
    /// <param name="newPath">is Directory Path</param>
    private static void Cut(string path, string newPath)
    {
        if (IsDir(path))
            Directory.Move(path, newPath);
        else
            File.Move(path, newPath);
    }

    /// <summary>
    /// Rename
    /// </summary>
    /// <param name="path">Path</param>
    /// <param name="newName">NewName</param>
    public static void Rename(string path, string newName)
    {
        var dic = Path.GetDirectoryName(path);
        if (IsFull(dic + "\\" + newName)) return;
        var file = FromPath(path);
        var extension = file is FileInfo ? file.Extension : "";
        var newPath = dic + "\\" + newName + extension;
        Cut(path, newPath);
    }

    private static bool IsDir(string filepath) => Directory.Exists(filepath) && !File.Exists(filepath);

    private static bool IsFull(string filePath) =>
        Directory.Exists(filePath) || File.Exists(filePath);

    private static FileSystemInfo FromPath(string path)
    {
        if (IsDir(path))
            return new DirectoryInfo(path);
        return new FileInfo(path);
    }
}
using System.Runtime.InteropServices;

namespace FileManager.Lib;

public static class OSInfo
{
    public static string OS
    {
        get
        {
            if (OperatingSystem.IsAndroid()) return "Android";
            if (OperatingSystem.IsBrowser()) return "Browser";
            if (OperatingSystem.IsLinux())return "Linux";
            if (OperatingSystem.IsIOS())return "IOS";
            if (OperatingSystem.IsWindows()) return "Window";
            if (OperatingSystem.IsMacCatalyst()) return "MacCatalyst";
            if (OperatingSystem.IsMacOS()) return "MacOS";
            if (OperatingSystem.IsTvOS()) return "TvOs";
            if (OperatingSystem.IsWatchOS()) return "WatchOS";
            return OperatingSystem.IsFreeBSD() ? "FreeBSD" : "Other";
        }
    }

    public static string TerminalPath => OperatingSystem.IsWindows() ? "cmd.exe" : "/bin/bash";
}
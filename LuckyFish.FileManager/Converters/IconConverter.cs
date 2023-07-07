using System;
using System.Globalization;
using Avalonia.Data.Converters;
using FileManager.Lib.FileModels;
using LuckyFish.FileManager.Models;
using Material.Icons;

namespace LuckyFish.FileManager.Converters;

public class IconConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string s)
        {
            return Enum.Parse<MaterialIconKind>(s);
        }
        
        if (value is IFileSystem)
        {
            if (value is DriveOperation)
            {
                return MaterialIconKind.ContentSave;
            }

            if (value is DirectoryOperation)
            {
                return MaterialIconKind.Folder;
            }

            if (value is FileOperation file)
            {
                switch (file.Extension)
                {
                    case ".cs":
                        return MaterialIconKind.LanguageCsharp;
                    case ".css":
                        return MaterialIconKind.LanguageCss3;
                    case ".c":
                        return MaterialIconKind.LanguageC;
                    case ".cpp":
                        return MaterialIconKind.LanguageCpp;
                    case ".java":
                        return MaterialIconKind.LanguageJava;
                    case ".js":
                        return MaterialIconKind.LanguageJavascript;
                    case ".html":
                        return MaterialIconKind.LanguageHtml5;
                    case ".xaml" or "axaml" or ".xml":
                        return MaterialIconKind.LanguageXaml;
                    case ".swift":
                        return MaterialIconKind.LanguageSwift;
                    case ".md":
                        return MaterialIconKind.LanguageMarkdown;
                    case ".ttf":
                        return MaterialIconKind.Language;
                    default:
                        return MaterialIconKind.File;
                }
            }
        }

        if (value is DriveSimpleOperation)
        {
            return MaterialIconKind.ContentSave;
        }

        if (value is CommonModel common)
        {
            if (common.Name == "Desktop")
            {
                return MaterialIconKind.DesktopWindows;
            }

            if (common.Name == "Music")
            {
                return MaterialIconKind.Music;
            }

            if (common.Name == "Videos")
            {
                return MaterialIconKind.Video;
            }

            if (common.Name == "Documents")
            {
                return MaterialIconKind.FileDocument;
            }

            if (common.Name == "Pictures")
            {
                return MaterialIconKind.Picture360;
            }
        }
        
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }
}
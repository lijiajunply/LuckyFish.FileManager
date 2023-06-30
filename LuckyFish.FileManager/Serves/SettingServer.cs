using System.IO;
using FileManager.Lib;
using LuckyFish.FileManager.Models;
using Newtonsoft.Json;

namespace LuckyFish.FileManager.Serves;

public static class SettingServer
{
    private static string FilePath => Path.Combine(CodeServer.CodePath,"Assets","Setting.json");
    public static SettingModel? Read()
        => JsonConvert.DeserializeObject<SettingModel>(File.ReadAllText(FilePath));

    public static string Save(SettingModel model)
    {
        string s = JsonConvert.SerializeObject(model);
        File.WriteAllText(FilePath,s);
        return s;
    }
}
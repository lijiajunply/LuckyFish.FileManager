using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FileManager.Lib;
using LuckyFish.FileManager.Serves;

namespace LuckyFish.FileManager.Models;

public class CommonModel
{
    public List<DirectoryInfo> Commons { get; set; } = new List<DirectoryInfo>();

    public void Save()
    {
        string json = JsonSerializer.Serialize(this);
        if (json == null) return;
        File.WriteAllText(CodeServer.CodePath + "\\Assets\\Common.json", json);
    }
    public static void CommonToJson(DirectoryInfo context)
    {
        CommonModel? model = JsonSerializer.Deserialize<CommonModel>(File.ReadAllText(CodeServer.CodePath+"/Assets/Common.json"));
        model.Commons.Add(context);
        model.Save();
    }

    public static List<DirectoryInfo> FromJson()
    {
        CommonModel? model = JsonSerializer.Deserialize<CommonModel>(File.ReadAllText(CodeServer.CodePath+"/Assets/Common.json"));
        List<DirectoryInfo> infos = new List<DirectoryInfo>();
        FileSystemOperation.GetCommonPath().ToList().ForEach(x => infos.Add(x));
        if (model.Commons != null) infos.AddRange(model.Commons);
        return infos;
    }
}
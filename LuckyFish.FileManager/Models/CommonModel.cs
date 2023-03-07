using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FileManager.Lib;
using LuckyFish.FileManager.Serves;

namespace LuckyFish.FileManager.Models;

public class CommonModel
{
    public List<string> Commons { get; set; } = new List<string>();

    public void Save()
    {
        string json = JsonSerializer.Serialize(this);
        if (json == null) return;
        File.WriteAllText(CodeServer.CodePath + "\\Assets\\Common.json", json);
    }

    public static void Remove(string path)
    {
        CommonModel? model = JsonSerializer.Deserialize<CommonModel>(File.ReadAllText(CodeServer.CodePath+"/Assets/Common.json"));
        model.Commons.Remove(path);
        model.Save();
    }
    
    public static void AddCommon(string context)
    {
        CommonModel? model = JsonSerializer.Deserialize<CommonModel>(File.ReadAllText(CodeServer.CodePath+"/Assets/Common.json"));
        model.Commons.Add(context);
        model.Save();
    }

    public static List<string> FromJson()
    {
        CommonModel? model = JsonSerializer.Deserialize<CommonModel>(File.ReadAllText(CodeServer.CodePath+"/Assets/Common.json"));
        List<string> infos = new List<string>();
        FileSystemOperation.GetCommonPath().ToList().ForEach(x => infos.Add(x.FullName));
        if (model.Commons != null) infos.AddRange(model.Commons);
        return infos;
    }
}
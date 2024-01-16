using System.Text.Json;
using AutomaticBroccoli.DataAccess.Models;

namespace AutomaticBroccoli.DataAccess.Repository;

public static class OpenLoopsRepository
{
    public static string DirectoryName = "./openLoops/";
    public static string DataDirecotory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DirectoryName);

    public static Guid Add(OpenLoop newOpenLoop)
    {
        Directory.CreateDirectory(DirectoryName);

        var json = JsonSerializer.Serialize(
            newOpenLoop, new JsonSerializerOptions { WriteIndented = true });

        var fileName = $"{newOpenLoop.Id}.json";
        var filePath = Path.Combine(DataDirecotory, fileName);

        File.WriteAllText(filePath, json);

        return newOpenLoop.Id;
    }
    public static OpenLoop[] Get()
    {
        var filesPath = Path.Combine(DataDirecotory);
        var files = Directory.GetFiles(filesPath);

        var openLoops = new List<OpenLoop>();

        foreach (var file in files)
        {
            var json = File.ReadAllText(file);

            var jsonObject = JsonSerializer.Deserialize<JsonElement>(json);

            var id = Guid.Parse(jsonObject.GetProperty("Id").GetString());
            var note = new Note(jsonObject.GetProperty("Note").GetProperty("Value").GetString());
            var createdDate = jsonObject.GetProperty("CreatedDate").GetProperty("Value").GetDateTimeOffset();

            var openLoop = new OpenLoop(id, note, createdDate);
            openLoops.Add(openLoop);
        }

        return openLoops.ToArray();
    }
}


using System.Text.Json;
using AutomaticBroccoli.DataAccess.Models;

namespace AutomaticBroccoli.DataAccess.Repository;

public static class OpenLoopsRepository
{
    private static string DirectoryName = "./openLoops/";
    private static string BaseDirecotory = AppDomain.CurrentDomain.BaseDirectory;

    public static Guid Add(OpenLoop newOpenLoop)
    {
        Directory.CreateDirectory(DirectoryName);

        var openLoopid = Guid.NewGuid();

        var json = JsonSerializer.Serialize(
            newOpenLoop with { Id = openLoopid}, 
            new JsonSerializerOptions { WriteIndented = true });

        var fileName = $"{openLoopid}.json";
        var filePath = Path.Combine(BaseDirecotory, DirectoryName, fileName);

        File.WriteAllText(filePath, json);

        return openLoopid;
    }
    public static OpenLoop[] Get()
    {
        var filesPath = Path.Combine(BaseDirecotory, DirectoryName);
        var files = Directory.GetFiles(filesPath);

        var openLoops = new List<OpenLoop>();

        foreach (var file in files)
        {
            var json = File.ReadAllText(file);

            var openLoop = JsonSerializer.Deserialize<OpenLoop>(json);

            if (openLoop is null)
            {
                throw new Exception("OpenLoop cannot be deserialized.");
            }

            openLoops.Add(openLoop);
        }
        return openLoops.ToArray();
    }
}


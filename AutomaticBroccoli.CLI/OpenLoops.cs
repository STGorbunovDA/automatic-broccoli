using System.Text.Json;

namespace AutomaticBroccoli.CLI;
public class OpenLoop
{
    public string Note { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}


public class OpenLoopsRepository
{
    private const string DirectoryName = "./openLoops/";

    public bool Add(OpenLoop newOpenLoop)
    {
        Directory.CreateDirectory(DirectoryName);

        var json = JsonSerializer.Serialize(newOpenLoop, new JsonSerializerOptions { WriteIndented = true });

        var fileName = $"{Guid.NewGuid()}.json";
        var filePath =  Path.Combine(DirectoryName, fileName);

        File.WriteAllText(filePath, json);

        return true;
    }
    public OpenLoop[] Get()
    {
        var files = Directory.GetFiles(DirectoryName);

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


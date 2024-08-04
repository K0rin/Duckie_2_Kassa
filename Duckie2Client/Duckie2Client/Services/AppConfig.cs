using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using Duckie2Client.Resources;
using Microsoft.Extensions.Configuration;

namespace Duckie2Client.Services;

public class DuckieConfig
{
    private const string ConfigFile = "appsettings.json";

    public DuckieConfig()
    {
        SearchConfigFile();
        CheckConfigFileIntegrity();

        Configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigFile)
            .Build();
    }

    public IConfiguration Configuration { get; }

    private static void SearchConfigFile()
    {
        if (File.Exists(ConfigFile)) return;

        // Create file with default settings with the 'InitialSetup' option set
        // to 'Show'.
        CreateDefaultSettingsFile();
    }

    private static void CreateDefaultSettingsFile()
    {
        // TODO: Handle error: cannot write file.
        var fileContent = ConfigFileDeafultContext.Value;
        using var sw = File.CreateText(ConfigFile);
        sw.WriteLine(fileContent);
    }

    private static void CheckConfigFileIntegrity()
    {
        try
        {
            using var sr = new StreamReader(ConfigFile);
            var json = sr.ReadToEnd();
            JsonNode.Parse(json);
        }
        catch (JsonException)
        {
            // Json structure is invalid.
            CreateDefaultSettingsFile();
        }
    }
}
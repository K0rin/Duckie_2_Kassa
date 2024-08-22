using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using Duckie2Client.Resources;
using Microsoft.Extensions.Configuration;

namespace Duckie2Client.Services;

public class DuckieConfig
{
    private const string CONFIG_FILE = "appsettings.json";

    public DuckieConfig()
    {
        SearchConfigFile();
        CheckConfigFileIntegrity();

        Configuration = new ConfigurationBuilder()
            .AddJsonFile(CONFIG_FILE)
            .Build();
    }

    public IConfiguration Configuration { get; }

    private static void SearchConfigFile()
    {
        if (File.Exists(CONFIG_FILE)) return;

        // Create the file with default settings with the 'InitialSetup' option set to 'Show'.
        CreateDefaultSettingsFile();
    }

    private static void CreateDefaultSettingsFile()
    {
        var fileContent = ConfigFileDeafultContext.Value;
        using var sw = File.CreateText(CONFIG_FILE);
        sw.WriteLine(fileContent);
    }

    private static void CheckConfigFileIntegrity()
    {
        try
        {
            using var sr = new StreamReader(CONFIG_FILE);
            var json = sr.ReadToEnd();
            JsonNode.Parse(json);
        }
        catch (JsonException)
        {
            // JSON structure is invalid.
            CreateDefaultSettingsFile();
        }
    }
}
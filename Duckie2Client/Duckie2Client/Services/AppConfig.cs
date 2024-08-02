using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;

namespace Duckie2Client.Services;

public class DuckieConfig
{
    public IConfiguration Configuration { get; }
    private const string ConfigFile = "appsettings.json";

    public DuckieConfig()
    {
        SearchConfigFile();
        CheckConfigFileIntegrity();

        Configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigFile)
            .Build();
    }

    private void SearchConfigFile()
    {
        if (File.Exists(ConfigFile)) return;

        // todo: Create file with default settings with the 'InitialSetup'
        // option set to 'Show'.
        CreateDefaultSettingsFile();
    }

    private void CreateDefaultSettingsFile()
    {
        // TODO: Handle error: cannot write file.
        var fileContent = Resources.ConfigFileDeafultContext.Value;
        using var sw = File.CreateText(ConfigFile);
        sw.WriteLine(fileContent);
    }

    public void CheckConfigFileIntegrity()
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
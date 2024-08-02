using System.IO;
using Microsoft.Extensions.Configuration;

namespace Duckie2Client.Services;

public class DuckieConfig
{
    public IConfiguration Configuration { get; }
    private const string ConfigFile = "_appsettings.json";


    public DuckieConfig()
    {
        SearchConfigFile();
        CheckConfigFileIntegrity();

        Configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigFile)
            .Build();

        // var title = Convert.ToInt32(Configuration["DatabaseServiceType"]);
        // if (title == (int)DatabaseServiceType.Standalone)
        // {
        //     
        // }
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
        // todo: implement
    }
}
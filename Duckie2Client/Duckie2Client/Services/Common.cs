using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;

namespace Duckie2Client.Services;

public static class Common
{
    public static void ExitApplication(int exitCode = 0)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
            lifetime.Shutdown(exitCode);
    }
}
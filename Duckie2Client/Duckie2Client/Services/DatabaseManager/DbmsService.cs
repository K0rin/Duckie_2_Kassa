using System;
using System.Diagnostics.CodeAnalysis;
using System.ServiceProcess;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client.Services.DatabaseManager;

public static class DbmsService
{
    public static void IsDbServiceRun()
    {
        PlatformSpecific.RunMethod(WindowsType, LinuxType);
    }

    [SuppressMessage(
        "Interoperability", "CA1416:Validate platform compatibility")]
    private static void WindowsType()
    {
        // ReSharper disable once StringLiteralTypo
        const string serviceName = "MSSQL$SQLEXPRESS";
        var sc = new ServiceController(serviceName);

        if (sc.Status == ServiceControllerStatus.Running) return;

        try
        {
            var throwError = sc.Status switch
            {
                ServiceControllerStatus.Stopped
                    or ServiceControllerStatus.StopPending =>
                    ErrorCodes.DbServiceStopped,
                ServiceControllerStatus.Paused
                    or ServiceControllerStatus.PausePending =>
                    ErrorCodes.DbServicePaused,
                /*
                 * todo: Timeout?
                 * Maybe need to wait some time and service will be available.
                 */
                ServiceControllerStatus.ContinuePending =>
                    ErrorCodes.DbServiceUnavailable,
                _ => throw new ArgumentOutOfRangeException(
                    $"Unknown service status: {sc.Status.ToString()}")
            };
            throw new DuckieException(throwError);
        }
        catch (InvalidOperationException)
        {
            throw new DuckieException(ErrorCodes.DbServiceUnavailable);
        }
    }

    private static void LinuxType()
    {
        throw new NotImplementedException();
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using Duckie2Client.Libs;
using Duckie2Client.Libs.Enums;

namespace Duckie2Client.Services;

public static class DatabaseService
{
    public static void IsDbServiceRun()

    {
        // todo: check DBMS running.
        // throw new DuckieException(ErrorCodes.DbServiceUnavailable);


        /* todo: refact:
         * создать функцию, на вход которой подаются два делегата: функции,
         * выполняемые в Windows и Linux соответственно.
         */
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            WindowsType();
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            // Reserved for linux.
            // LinuxType();
        }
        else
        {
            throw new PlatformNotSupportedException(
                "Unsupported operating system.");
        }
    }

    [SuppressMessage(
        "Interoperability", "CA1416:Validate platform compatibility")]
    private static void WindowsType()
    {
        // ReSharper disable once StringLiteralTypo
        const string serviceName = "MSSQL$SQLEXPRESS";
        var sc = new ServiceController(serviceName);

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
}
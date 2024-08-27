using System;
using System.Runtime.InteropServices;

namespace Duckie2Client.Libs;

public static class PlatformSpecific
{
    public delegate void LinuxDelegate();

    public delegate bool PlatformDelegate(string name);

    public delegate void WindowsDelegate();

    public static void RunMethod(
        PlatformDelegate windowsPlatformFunction,
        PlatformDelegate linuxPlatformFunction,
        string name,
        out bool result)
    {
        PlatformDelegate platform;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) platform = windowsPlatformFunction;
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) platform = linuxPlatformFunction;
        else throw new PlatformNotSupportedException("Unsupported platform");

        result = platform(name);
    }

    public static void RunMethod(WindowsDelegate windowsDelegate, LinuxDelegate linuxDelegate)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) windowsDelegate();
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) linuxDelegate();
        else throw new PlatformNotSupportedException("Unsupported platform");
    }
}
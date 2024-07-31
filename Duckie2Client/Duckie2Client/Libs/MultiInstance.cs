using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Duckie2Client.Libs;

public class MultiInstance
{
    private const string NamePrefix = "duckie-";
    private FileStream? _linuxLockFile;
    private string? _linuxLockFilePath;

    /// <summary>
    /// Checks that application has only one instance running in current time.
    /// </summary>
    /// <param name="mutexName">
    /// Name for a mutex. Each application mode has its own name, which is used
    /// for identifying application instances.
    /// </param>
    /// <returns>
    /// True - Application has no any running instances and can be run.<br/>
    /// False - Application already has a running instance and cannot be run.
    /// </returns>
    /// <exception cref="PlatformNotSupportedException">
    /// Application is being run on an unsupported operating system.
    /// </exception>
    public bool IsSingleInstance(string mutexName)
    {
        bool result;
        var name = $"{NamePrefix}{mutexName}";

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            result = WindowsType(name);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            result = LinuxType(name);
        }
        else
        {
            throw new PlatformNotSupportedException(
                "Unsupported operating system.");
        }

        return result;
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
#pragma warning disable CA1822
    private bool WindowsType(string mutexName)
#pragma warning restore CA1822
    {
        try
        {
            // Try to open existing mutex.
            Mutex.OpenExisting(mutexName);
        }
        catch
        {
            // If exception occurred, there is no such mutex.
            var m = new Mutex(true, mutexName);
            // Only one instance.
            return true;
        }

        return false;
    }

    private bool LinuxType(string mutexName)
    {
        _linuxLockFilePath = $"/tmp/{mutexName}.lock";

        try
        {
            _linuxLockFile = new FileStream(
                _linuxLockFilePath,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.None);
#pragma warning disable CA1416
            _linuxLockFile.Lock(0, 0);
#pragma warning restore CA1416
            return true;
        }
        catch (IOException)
        {
            return false;
        }
    }

    public void UnlockFile()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return;
        _linuxLockFile?.Unlock(0, 0);
        _linuxLockFile?.Close();
        if (_linuxLockFilePath != null) File.Delete(_linuxLockFilePath);
    }
}
using System.Threading;

namespace Duckie2Client.Libs;

public static class MultiInstance
{
    public static bool IsSingleInstance(string mutexName)
    {
        var name = $"duckie{mutexName}";
        try
        {
            // Try to open existing mutex.
            Mutex.OpenExisting(name);
        }
        catch
        {
            // If exception occurred, there is no such mutex.
            var _m = new Mutex(true, name);
            // Only one instance.
            return true;
        }

        return false;
    }
}
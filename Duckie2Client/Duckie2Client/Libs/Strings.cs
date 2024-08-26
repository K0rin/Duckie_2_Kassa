using System.Linq;

namespace Duckie2Client.Libs;

/// <summary>Contains functionality for working with strings.</summary>
public static class Strings
{
    /// <summary>Returns a string where the first letter in the string is an uppercase letter.</summary>
    /// <param name="value">Initial string.</param>
    public static string GetFirstTitleCase(string value)
    {
        var firstLetter = value.First().ToString().ToUpper();
        var restLetters = value.Substring(1, value.Length - 1);
        var output = $"{firstLetter}{restLetters}";
        return output;
    }
}
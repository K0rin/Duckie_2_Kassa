using System.Collections.Generic;
using DynamicData;

namespace Duckie2Client.Libs.Enums;

public enum RuntimeErrors
{
    NameScopeNotFound,
    CannotFindAnElementWithName
}

public static class RuntimeErrorsExtensions
{
    private static readonly Dictionary<RuntimeErrors, string> Messages = new()
    {
        { RuntimeErrors.NameScopeNotFound, "Name scope not found." },
        { RuntimeErrors.CannotFindAnElementWithName, "Cannot find an element with name '{1}'." }
    };

    public static string GetMessage(this RuntimeErrors re, string[] substitution)
    {
        var output = SubstituteString(Messages[re], ref substitution);
        return output;
    }

    public static string GetMessage(this RuntimeErrors re)
    {
        var output = Messages[re];
        return output;
    }

    private static string SubstituteString(string text, ref string[] substitutions)
    {
        if (substitutions.Length.Equals(0)) return text;

        var output = "";

        foreach (var s in substitutions)
        {
            var i = substitutions.IndexOf(s) + 1;
            output = text.Replace($"{{{i}}}", s);
        }

        return output;
    }
}
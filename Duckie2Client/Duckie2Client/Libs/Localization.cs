using System;
using System.Globalization;
using System.Linq.Expressions;
using Duckie2Client.Libs.Enums;
using Duckie2Client.Resources;
using Duckie2Client.Services;

namespace Duckie2Client.Libs;

public interface IStrategy
{
    string? DoAlgorithm(string data, CultureInfo installedCulture);
}

public class Context(IStrategy strategy)
{
    private const string NoTranslationLabel = "<NO_TRANSLATION>";

    public string GetString(string stringName)
    {
        // If there are special option for language in the settings file, load
        // appropriate culture.
        // Otherwise, load system culture.

        var userDefinedCulture =
            new DuckieConfig().Configuration[
                SettingsFileOptions.UiLanguage];

        // TODO: LINUX
        var installedCulture = string.IsNullOrEmpty(userDefinedCulture)
            // Get currently using by operating system culture.
            ? CultureInfo.InstalledUICulture
            : CultureInfo.GetCultureInfo(userDefinedCulture);
        var cultureInfo = CultureInfo.GetCultureInfo(
            installedCulture.TwoLetterISOLanguageName);
        var result = strategy.DoAlgorithm(stringName, cultureInfo);

        // If a resource file has no translation for a denoted string, return
        // placeholder text "<NO_TRANSLATION>".
        return result ?? NoTranslationLabel;
    }
}

public class ErrorMessagesGettingString : IStrategy
{
    public string? DoAlgorithm(string someText, CultureInfo cultureInfo)
    {
        return ErrorMessages.ResourceManager.GetString(someText, cultureInfo);
    }
}

public class UserInterfaceGettingString : IStrategy
{
    public string? DoAlgorithm(string someText, CultureInfo cultureInfo)
    {
        return UserInterface.ResourceManager.GetString(someText, cultureInfo);
    }
}

public static class Localization
{
    public static string GetString(Expression<Func<string>> nameGetter,
        ResourceTypes resourceType)
    {
        string getterName;

        if (nameGetter.Body is MemberExpression memberExpression)
            getterName = memberExpression.Member.Name;
        else
            throw new Exception(
                "Error occured during getting class getter name.");

        var result = GetString(getterName, resourceType);

        return result;
    }

    public static string GetString(
        string stringName,
        ResourceTypes resourceType)
    {
        var context = resourceType switch
        {
            ResourceTypes.ErrorMessages => new Context(
                new ErrorMessagesGettingString()),
            ResourceTypes.UserInterface => new Context(
                new UserInterfaceGettingString()),
            /* NOTE:
             Add here other resource courses.
             Create new class for a strategy named
             "{resource name}GettingString."
             See the class ErrorMessagesGettingString for sample.
            */
            _ => throw new ArgumentOutOfRangeException(nameof(resourceType),
                resourceType, null)
        };

        var result = context.GetString(stringName);

        return result;

        // Check if installed culture exists in localization resources.
        // Возможно, проверять наличие файлов локализации только в режиме отладки.
        // CheckCultureHasTranslations(installedCulture);
    }

    // private static void CheckCultureHasTranslations(CultureInfo cultureName)
    // {
    //     var cultureList = GetAvailableCultures();
    //
    //     if (!cultureList.Contains(cultureName))
    //         throw new Exception(
    //             "There is no translation for a denoted culture.");
    // }
    //
    // private static IEnumerable<CultureInfo> GetAvailableCultures()
    // {
    //     var result = new List<CultureInfo>();
    //     var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
    //
    //     foreach (var culture in cultures)
    //     {
    //         if (culture.Equals(CultureInfo.InvariantCulture)) continue;
    //
    //         var rs = Resources1.ResourceManager.GetResourceSet(
    //             culture,
    //             true,
    //             false);
    //
    //         if (rs != null) result.Add(culture);
    //     }
    //
    //     return result;
    // }
}
using System.Globalization;
using Duckie2Client.Resources;

namespace Duckie2Client.Libs;

public static class Localization
{
    public static string GetString(string stringName)
    {
        // Get currently using by operating system culture.
        // TODO: LINUX
        var installedCulture = CultureInfo.InstalledUICulture;

        // Check if installed culture exists in localization resources.
        // Возможно, проверять наличие файлов локализации только в режиме отладки.
        // CheckCultureHasTranslations(installedCulture);

        var cultureInfo = CultureInfo.GetCultureInfo(
            installedCulture.TwoLetterISOLanguageName);

        var result =
            Resources1.ResourceManager.GetString(stringName, cultureInfo);

        // If a resource file has no translation for a denoted string, return
        // placeholder text "<NO_TRANSLATION>".
        return result ?? "<NO_TRANSLATION>";
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
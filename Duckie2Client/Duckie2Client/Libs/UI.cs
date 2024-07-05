using System;
using Avalonia;
using Avalonia.Controls;

namespace Duckie2Client.Libs;

public static class Ui
{
    public static void CenterWindowOnScreen(Window window)
    {
        // Get the primary screen's working area
        var workingArea = window.Screens.Primary!.WorkingArea;

        // Adjust working area size based on the scale factor
        var scaleFactor = GetScaleFactor();
        var scaledScreenWidth = workingArea.Width / scaleFactor;
        var scaledScreenHeight = workingArea.Height / scaleFactor;

        // Calculate the centered position
        var centerX = (scaledScreenWidth - window.Width) / 2;
        var centerY = (scaledScreenHeight - window.Height) / 2;

        // Apply the scaled position
        window.Position = new PixelPoint((int)centerX, (int)centerY);
    }
    private static double GetScaleFactor()
    {
        // ReSharper disable once InconsistentNaming
        const string SCALE_FACTOR ="AVALONIA_GLOBAL_SCALE_FACTOR";
        
        // Retrieve the scale factor from the environmental variable
        var scaleFactorStr = Environment.GetEnvironmentVariable(SCALE_FACTOR);
        // Scale factor by default is 1.
        var scaleFactor = 1.0;

        if (!string.IsNullOrEmpty(scaleFactorStr) &&
            double.TryParse(scaleFactorStr, out var parsedScaleFactor))
        {
            scaleFactor = parsedScaleFactor;
        }

        return scaleFactor;
    }
}
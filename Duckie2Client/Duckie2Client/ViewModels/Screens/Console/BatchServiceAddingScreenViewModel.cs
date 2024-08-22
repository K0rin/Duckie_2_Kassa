using Duckie2Client.ViewModels.Base;

namespace Duckie2Client.ViewModels.Screens.Console;

public class BatchServiceAddingScreenViewModel : ViewModelBase

{
    public void OnScreenClose()
    {
        System.Console.WriteLine("batch service close");
    }
}
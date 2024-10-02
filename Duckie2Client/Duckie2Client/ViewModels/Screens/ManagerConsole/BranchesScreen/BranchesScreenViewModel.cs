using System.Collections.Generic;
using System.Reactive;
using Duckie2Client.Services.DbmsService;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Duckie2Client.ViewModels.Base;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole.BranchesScreen;

public class BranchesScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    public List<string>? DataPayload { get; set; }
    public static object DialogIdentifier => "BranchesScreenDialogs";
    public ReactiveCommand<Unit, Unit> AddBranchCommand { get; }

    public BranchesScreenViewModel()
    {
        AddBranchCommand = ReactiveCommand.Create(AddBranchCommandExecute);
    }

    private void AddBranchCommandExecute()
    {
        var branchRecordBuilder = new BranchRecordBuilder();
        branchRecordBuilder.AddName("Branch 3");
        branchRecordBuilder.AddAddress("Branch 3 Address");
        new Branches().Create(branchRecordBuilder);
    }

    // todo: refact: Часть интерфейса. Можно не реализовывать, если не надо.
    public void OnScreenClose()
    {
        // todo: Запрос на сохранение не сохраненных данных.
        // Console.WriteLine(@"batch service close");
    }
}
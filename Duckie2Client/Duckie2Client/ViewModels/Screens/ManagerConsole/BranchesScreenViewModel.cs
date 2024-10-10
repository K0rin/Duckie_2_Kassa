using System.Collections.Generic;
using System.Reactive;
using Duckie2Client.Services.DbmsService;
using Duckie2Client.Services.DbmsService.Records.Builders;
using Duckie2Client.ViewModels.Base;
using Duckie2Client.Views.Dialogs;
using ReactiveUI;

namespace Duckie2Client.ViewModels.Screens.ManagerConsole;

public class BranchesScreenViewModel : ViewModelBase, ITabViewModel<List<string>>
{
    public List<string>? DataPayload { get; set; }
    public static object DialogIdentifier => "BranchesScreenDialogs";
    public ReactiveCommand<Unit, Unit> AddBranchCommand { get; }

    // todo: DRY
    private ErrorDialog? _errorDialog;

    public BranchesScreenViewModel()
    {
        AddBranchCommand = ReactiveCommand.Create(AddBranchCommandExecute);
    }

    private void AddBranchCommandExecute()
    {
        var branchRecordBuilder = new BranchRecordBuilder();
        branchRecordBuilder.AddName("Branch 2");
        branchRecordBuilder.AddAddress("Branch 2 Address");
        new Branches().Create(branchRecordBuilder);
    }
}
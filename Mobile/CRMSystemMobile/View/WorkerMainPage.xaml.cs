using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class WorkerMainPage
{
    private readonly WorkerMainViewModel _viewModel;

    public WorkerMainPage(WorkerMainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.LoadInitialCommand.ExecuteAsync(null);
    }
}
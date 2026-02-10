using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class WorkerMainPage : ContentPage
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

        if (_viewModel != null)
        {
            await _viewModel.LoadInitialCommand.ExecuteAsync(null);
        }
    }
}
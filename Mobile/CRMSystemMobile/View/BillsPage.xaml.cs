using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class BillsPage : ContentPage
{
    private readonly BillsViewModel _viewModel;

    public BillsPage(BillsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel.LoadInitialCommand.CanExecute(null))
        {
            await _viewModel.LoadInitialCommand.ExecuteAsync(null);
        }
    }
}
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
        await _viewModel.LoadBillsCommand.ExecuteAsync(null);
    }
}
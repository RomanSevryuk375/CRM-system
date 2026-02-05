using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class PaymentsPage : ContentPage
{
    private readonly PaymentsViewModel _viewModel;

    public PaymentsPage(PaymentsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (_viewModel.LoadInitialCommand.CanExecute(null))
            await _viewModel.LoadInitialCommand.ExecuteAsync(null);
    }
}
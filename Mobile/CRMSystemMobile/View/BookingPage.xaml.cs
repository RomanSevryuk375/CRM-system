using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class BookingPage : ContentPage
{
    private readonly BookingViewModel _viewModel;

    public BookingPage(BookingViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataCommand.ExecuteAsync(null);
    }
}
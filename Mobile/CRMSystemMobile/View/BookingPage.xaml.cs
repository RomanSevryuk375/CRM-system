using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class BookingPage : ContentPage
{
    public BookingPage(BookingViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        //await _viewModel.LoadDataCommand.ExecuteAsync(null);
    }
}
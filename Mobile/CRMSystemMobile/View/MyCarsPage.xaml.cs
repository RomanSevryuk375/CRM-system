using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class MyCarsPage : ContentPage
{
    private readonly MyCarsViewModel _viewModel;

    public MyCarsPage(MyCarsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadCarsCommand.ExecuteAsync(null);
    }
}
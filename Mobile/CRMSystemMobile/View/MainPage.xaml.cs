using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class MainPage
{
    private readonly MainViewModel _viewModel;

    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();
        _viewModel = mainViewModel;
        BindingContext = _viewModel;
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
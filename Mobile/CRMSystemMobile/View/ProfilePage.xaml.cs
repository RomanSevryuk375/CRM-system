using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileViewModel _viewModel;

    public ProfilePage(ProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadProfile();
        if (BindingContext is ProfileViewModel vm)
        {
            await vm.LoadProfileCommand.ExecuteAsync(null);
        }
    }
}
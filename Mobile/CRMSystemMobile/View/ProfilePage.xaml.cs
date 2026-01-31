using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel viewModel)
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ProfileViewModel vm)
        {
            await vm.LoadProfileCommand.ExecuteAsync(null);
        }
    }
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CRMSystemMobile.ViewModels;

public partial class RegistrationViewModel : ObservableObject
{
    [ObservableProperty] private string nameSurname = "";
    [ObservableProperty] private string phoneNimber = "";
    [ObservableProperty] private string email = "";
    [ObservableProperty] private string userLogin = "";
    [ObservableProperty] private string userPassword = "";

    //[RelayCommand]
    //private async Task Register()
    //{

    //}

    [RelayCommand]
    private async Task OnGoToLogin() => await Shell.Current.GoToAsync("//LoginPage");
}

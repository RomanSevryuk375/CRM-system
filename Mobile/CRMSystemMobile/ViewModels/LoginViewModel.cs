using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Login;

namespace CRMSystemMobile.ViewModel;

public partial class LoginViewModel(LoginService loginService) : ObservableObject
{
    [ObservableProperty] private string userLogin = "";

    [ObservableProperty] private string userPassword = "";

    [RelayCommand]
    private async Task Login()
    {
        var request = new LoginRequest
        {
            Login = UserLogin,
            Password = UserPassword
        };

        var response = await loginService.LoginUser(request);

        if (response != null)
        {
            //await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", "Неверный логин или пароль", "ОК");
        }
    }

    [RelayCommand]
    private async Task OnGoToRegistration() => await Shell.Current.GoToAsync("//RegistrationPage");
}

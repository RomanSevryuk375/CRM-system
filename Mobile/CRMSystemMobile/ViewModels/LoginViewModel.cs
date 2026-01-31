using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Login;

namespace CRMSystemMobile.ViewModel;

public partial class LoginViewModel(LoginService loginService) : ObservableObject
{
    [ObservableProperty]
    public partial string UserLogin { get; set; }

    [ObservableProperty]
    public partial string UserPassword { get; set; }

    [ObservableProperty]
    private bool _isPasswordHidden = true;

    [ObservableProperty]
    private string _passwordIcon = "eye_hide.png";

    [RelayCommand]
    private void TogglePassword()
    {
        IsPasswordHidden = !IsPasswordHidden;
        PasswordIcon = IsPasswordHidden ? "eye_hide.png" : "visible_hide.png";
    }

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
            await Shell.Current.GoToAsync("//MainPage");
        else
            await Shell.Current.DisplayAlert("Ошибка", "Неверный логин или пароль", "ОК");
    }

    [RelayCommand]
    private async Task OnGoToRegistration() => await Shell.Current.GoToAsync("RegistrationPage");
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Extentions;
using CRMSystemMobile.Services;
using Shared.Contracts.Login;

namespace CRMSystemMobile.ViewModel;

public partial class LoginViewModel(LoginService loginService, IdentityService identityService) : ObservableObject
{
    [ObservableProperty]
    public partial string UserLogin { get; set; }

    [ObservableProperty]
    public partial string UserPassword { get; set; }

    [ObservableProperty]
    public partial bool IsPasswordHidden { get; set; } = true;

    [ObservableProperty]
    public partial string PasswordIcon { get; set; } = "eye_hide.png";

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
        {
            var (_, roleId) = await identityService.GetProfileIdAsync();

            switch (roleId)
            {
                case 3:
                    await Shell.Current.GoToAsync("//WorkerMainPage");
                    break;
                case 2:
                    await Shell.Current.GoToAsync("//MainPage");
                    break;
                case 1:
                    await Shell.Current.GoToAsync("//MainPage");
                    break;
                default:
                    await Shell.Current.DisplayAlert("Ошибка", "Неверный логин или пароль", "ОК");
                    break;
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", "Неверный логин или пароль", "ОК");
        }
    }

    [RelayCommand]
    private async Task OnGoToRegistration() => await Shell.Current.GoToAsync("RegistrationPage");
}

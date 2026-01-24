using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Client;
using Shared.Contracts.Login;
using Shared.Enums;

namespace CRMSystemMobile.ViewModels;

public partial class RegistrationViewModel(
    RegistrationService registrationService,
    LoginService loginService) : ObservableObject
{
    [ObservableProperty]
    public partial string NameSurname { get; set; }

    [ObservableProperty]
    public partial string ClientPhoneNumber { get; set; }

    [ObservableProperty]
    public partial string ClientEmail { get; set; }

    [ObservableProperty]
    public partial string UserLogin { get; set; }

    [ObservableProperty]
    public partial string UserPassword { get; set; }

    [RelayCommand]
    private async Task Register()
    {
        var words = NameSurname.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var request = new ClientRegisterRequest
        {
            Name = words.Length > 0 ? words[0] : "Имя",
            Surname = words.Length > 0 ? words[1] : "Фамилия",
            PhoneNumber = ClientPhoneNumber,
            Email = ClientEmail,
            RoleId = (int)RoleEnum.Client,
            Login = UserLogin,
            Password = UserPassword,
        };

        var isReg = await registrationService.RegisterUser(request);

        if (isReg)
        {
            var loginRequest = new LoginRequest
            {
                Login = UserLogin,
                Password = UserPassword,
            };

            var result = await loginService.LoginUser(loginRequest);
            if (result != null)
                await Shell.Current.GoToAsync("//MainPage");
        }
        else
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось зарегистрироваться", "ОК");
        
    }

    [RelayCommand]
    private async Task OnGoToLogin() => await Shell.Current.GoToAsync("//LoginPage");
}

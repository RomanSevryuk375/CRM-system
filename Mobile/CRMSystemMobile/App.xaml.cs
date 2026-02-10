using CRMSystemMobile.Extentions;

namespace CRMSystemMobile;

public partial class App : Application
{
    private readonly AppShell _shell;
    private readonly IdentityService _identityService;

    public App(AppShell shell, IdentityService identityService)
    {
        InitializeComponent();
        _identityService = identityService;
        _shell = shell;
        CheckAutoLogin();
    }

    private async void CheckAutoLogin()
    {
        var token = await SecureStorage.Default.GetAsync("jwt_token");

        if (!string.IsNullOrEmpty(token) && _identityService.IsTokenValid(token))
        {
            var (_, roleId) = await _identityService.GetProfileIdAsync();

            switch (roleId)
            {
                case 3:
                    await Shell.Current.GoToAsync("//WorkerMainPage");
                    break;
                case 2 & 1:
                    await Shell.Current.GoToAsync("//MainPage");
                    break;
                default:
                    await Shell.Current.DisplayAlert("Ошибка", "Неверный логин или пароль", "ОК");
                    break;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(token))
            {
                SecureStorage.Default.Remove("jwt_token");
            }
        }
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(_shell);
    }
}
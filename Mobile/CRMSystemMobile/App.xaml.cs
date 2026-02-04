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
            await Shell.Current.GoToAsync("//MainPage");
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
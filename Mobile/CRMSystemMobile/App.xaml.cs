namespace CRMSystemMobile;

public partial class App : Application
{
    private readonly AppShell _shell;

    public App(AppShell shell)
    {
        InitializeComponent();
        _shell = shell;
        _ = CheckAuthAndRedirect();
    }

    private async Task CheckAuthAndRedirect()
    {
        try
        {
            var token = await SecureStorage.Default.GetAsync("jwt_token");

            if (!string.IsNullOrEmpty(token))
            {
                await _shell.GoToAsync("//MainPage");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Auth check failed: {ex.Message}");
        }
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(_shell);
    }
}
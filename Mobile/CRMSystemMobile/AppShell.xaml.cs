using CRMSystemMobile.View;

namespace CRMSystemMobile;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("RegistrationPage", typeof(RegistrationPage));
        Routing.RegisterRoute("LoginPage", typeof(LoginPage));
    }
}

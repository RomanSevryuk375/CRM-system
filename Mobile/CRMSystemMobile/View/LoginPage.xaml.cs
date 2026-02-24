using LoginViewModel = CRMSystemMobile.ViewModels.LoginViewModel;

namespace CRMSystemMobile.View;

public partial class LoginPage
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }

    private void OnLoginCompleted(object sender, EventArgs e) => PasswordEntry.Focus();

    private void OnPasswordToggled()
    {
        PasswordEntry.CursorPosition = PasswordEntry.Text?.Length ?? 0;
    }
}
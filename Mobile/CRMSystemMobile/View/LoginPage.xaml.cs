using CRMSystemMobile.ViewModel;

namespace CRMSystemMobile;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel loginViewModel)
    {
        InitializeComponent();
        BindingContext = loginViewModel;
    }

    private void OnLoginCompleted(object sender, EventArgs e) => PasswordEntry.Focus();

    private void OnPasswordToggled(object sender, EventArgs e)
    {
        PasswordEntry.CursorPosition = PasswordEntry.Text?.Length ?? 0;
    }
}

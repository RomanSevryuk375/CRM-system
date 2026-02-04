using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage(RegistrationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void OnNameCompleted(object sender, EventArgs e)
    {
        PhoneEntry.Focus();
    }

    private void OnPhoneCompleted(object sender, EventArgs e)
    {
        EmailEntry.Focus();
    }

    private void OnEmailCompleted(object sender, EventArgs e)
    {
        LoginEntry.Focus();
    }

    private void OnLoginCompleted(object sender, EventArgs e)
    {
        PasswordEntry.Focus();
    }

    private void OnPasswordToggled(object sender, EventArgs e)
    {
        PasswordEntry.CursorPosition = PasswordEntry.Text?.Length ?? 0;
    }
}
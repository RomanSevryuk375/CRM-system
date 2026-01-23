using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage(RegistrationViewModel registrationViewModel)
	{
		InitializeComponent();
		BindingContext = registrationViewModel;
	}
}
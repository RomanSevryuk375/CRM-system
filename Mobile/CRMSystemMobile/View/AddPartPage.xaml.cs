using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class AddPartPage : ContentPage
{
	public AddPartPage(AddPartViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
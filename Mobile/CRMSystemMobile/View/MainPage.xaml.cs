using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel mainViewModel)
	{
		InitializeComponent();
		BindingContext = mainViewModel;
	}
}
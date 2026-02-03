using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class AddCarPage : ContentPage
{
    public AddCarPage(AddCarViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
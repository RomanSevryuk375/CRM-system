using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class CarDetailsPage : ContentPage
{
    public CarDetailsPage(CarDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
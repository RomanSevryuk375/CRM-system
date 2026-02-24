using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class CarDetailsPage
{
    public CarDetailsPage(CarDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
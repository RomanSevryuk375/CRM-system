using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class AddCarPage
{
    public AddCarPage(AddCarViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
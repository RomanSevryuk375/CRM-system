using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class OrderDetailsPage
{
    public OrderDetailsPage(OrderDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
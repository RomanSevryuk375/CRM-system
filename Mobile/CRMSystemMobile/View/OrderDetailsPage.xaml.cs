using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class OrderDetailsPage : ContentPage
{
    public OrderDetailsPage(OrderDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
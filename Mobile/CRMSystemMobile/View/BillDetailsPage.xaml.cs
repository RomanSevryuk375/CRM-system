using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class BillDetailsPage : ContentPage
{
    public BillDetailsPage(BillDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
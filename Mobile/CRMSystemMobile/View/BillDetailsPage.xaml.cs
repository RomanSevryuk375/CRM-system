using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class BillDetailsPage
{
    public BillDetailsPage(BillDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
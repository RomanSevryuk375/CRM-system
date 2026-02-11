using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class AddProposalPage : ContentPage
{
    public AddProposalPage(AddProposalViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
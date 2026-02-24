using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class AddProposalPage
{
    public AddProposalPage(AddProposalViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
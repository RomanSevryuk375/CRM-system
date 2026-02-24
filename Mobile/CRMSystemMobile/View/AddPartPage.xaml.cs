using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class AddPartPage
{
    public AddPartPage(AddPartViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
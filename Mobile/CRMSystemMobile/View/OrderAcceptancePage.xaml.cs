using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class OrderAcceptancePage
{
    public OrderAcceptancePage(OrderAcceptanceViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
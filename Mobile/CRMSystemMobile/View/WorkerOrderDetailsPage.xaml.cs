using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class WorkerOrderDetailsPage
{
    public WorkerOrderDetailsPage(WorkerOrderDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
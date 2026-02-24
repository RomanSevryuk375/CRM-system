using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class WorkerProfilePage
{
    public WorkerProfilePage(WorkerProfileViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
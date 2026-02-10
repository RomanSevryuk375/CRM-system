using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class WorkerProfilePage : ContentPage
{
    public WorkerProfilePage(WorkerProfileViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
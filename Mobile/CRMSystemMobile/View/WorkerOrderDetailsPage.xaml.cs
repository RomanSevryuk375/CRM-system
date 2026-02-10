using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class WorkerOrderDetailsPage : ContentPage
{
    public WorkerOrderDetailsPage(WorkerOrderDetailsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
using CRMSystemMobile.ViewModels;

namespace CRMSystemMobile.View;

public partial class WorkerSchedulePage : ContentPage
{
    private readonly WorkerScheduleViewModel _viewModel;

    public WorkerSchedulePage(WorkerScheduleViewModel vm)
    {
        InitializeComponent();
        BindingContext = _viewModel = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadSchedulesCommand.ExecuteAsync(null);
    }
}
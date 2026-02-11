using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Schedule;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class WorkerScheduleViewModel(ScheduleService scheduleService) : ObservableObject
{
    public ObservableCollection<ScheduleResponse> Schedules { get; } = [];

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }

    [RelayCommand]
    private async Task LoadSchedules()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var items = await scheduleService.GetMySchedules();

            Schedules.Clear();
            if (items != null)
            {
                foreach (var item in items)
                {
                    Schedules.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось загрузить расписание", "ОК");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
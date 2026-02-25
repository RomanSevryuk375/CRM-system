using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Schedule;
using System.Collections.ObjectModel;
using CRMSystemMobile.Extensions;
using Shared.Filters;

namespace CRMSystemMobile.ViewModels;

public partial class WorkerScheduleViewModel(ScheduleService scheduleService, IdentityService identityService) : ObservableObject
{
    public ObservableCollection<ScheduleResponse> Schedules { get; } = [];

    [ObservableProperty] public partial bool IsBusy { get; set; }

    [ObservableProperty] public partial bool IsRefreshing { get; set; }

    [RelayCommand]
    private async Task LoadSchedules()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var (profileId, _) = await identityService.GetProfileIdAsync();

            var filter = new ScheduleFilter
            (
                WorkerIds: profileId > 0 ? [(int)profileId] : [], 
                ShiftIds: [],       
                SortBy: "date",     
                Page: 1,            
                Limit: 100,        
                IsDescending: true  
            );

            var (items, count) = await scheduleService.GetMySchedules(filter);

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
            await Shell.Current.DisplayAlert("Ошибка", $"Не удалось загрузить расписание. {ex.Message}", "ОК");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private static async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
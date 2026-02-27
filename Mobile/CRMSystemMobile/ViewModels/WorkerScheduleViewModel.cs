using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Absence;
using Shared.Contracts.Schedule;
using Shared.Contracts.Shift;
using Shared.Filters;
using System.Collections.ObjectModel;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.ViewModels;

public partial class WorkerScheduleViewModel(
    ScheduleService scheduleService,
    AbsenceService absenceService,
    IdentityService identityService,
    ShiftService shiftService)
    : ObservableObject
{
    private Dictionary<int, ShiftResponse>? _cachedShiftsDict;

    private ObservableCollection<object> CalendarItems { get; } = [];

    [ObservableProperty] public partial bool IsBusy { get; set; }
    [ObservableProperty] public partial bool IsRefreshing { get; set; }
    [ObservableProperty] public partial bool IsLoadingMore { get; set; }

    private int _currentPage = 1;
    private const int PageSize = 20;

    private bool _hasMoreSchedules = true;
    private bool _hasMoreAbsences = true;

    [RelayCommand]
    private async Task LoadInitial()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            _currentPage = 1;
            _hasMoreSchedules = true;
            _hasMoreAbsences = true;

            CalendarItems.Clear();

            if (_cachedShiftsDict == null)
            {
                var shifts = await shiftService.GetAllShifts();
                _cachedShiftsDict = shifts?.ToDictionary(k => k.Id, v => v) ?? [];
            }

            await LoadDataInternal();
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task LoadNextPage()
    {
        if (IsBusy || IsLoadingMore || (!_hasMoreSchedules && !_hasMoreAbsences)) return;

        IsLoadingMore = true;
        try
        {
            _currentPage++;
            await LoadDataInternal();
        }
        finally
        {
            IsLoadingMore = false;
        }
    }

    private async Task LoadDataInternal()
    {
        try
        {
            var (profileId, _) = await identityService.GetProfileIdAsync();
            if (profileId <= 0) return;

            var schedulesTask = _hasMoreSchedules
                ? scheduleService.GetMySchedules(new ScheduleFilter(
                    WorkerIds: [(int)profileId],
                    ShiftIds: [],
                    SortBy: "date",
                    Page: _currentPage,
                    Limit: PageSize,
                    IsDescending: true))
                : Task.FromResult<(List<ScheduleResponse>?, int)>((null, 0));

            var absencesTask = _hasMoreAbsences
                ? absenceService.GetMyAbsences(new AbsenceFilter(
                    WorkerIds: [(int)profileId],
                    SortBy: "startDate",
                    Page: _currentPage,
                    Limit: PageSize,
                    IsDescending: true))
                : Task.FromResult<(List<AbsenceResponse>?, int)>((null, 0));

            await Task.WhenAll(schedulesTask, absencesTask);

            var (schedules, _) = await schedulesTask;
            var (absences, _) = await absencesTask;

            if ((schedules?.Count ?? 0) < PageSize) _hasMoreSchedules = false;
            if ((absences?.Count ?? 0) < PageSize) _hasMoreAbsences = false;

            if ((schedules == null || schedules.Count == 0) && (absences == null || absences.Count == 0))
                return;

            var batchList = new List<object>();
            var today = DateTime.Today;

            if (schedules != null)
            {
                foreach (var schedule in schedules)
                {
                    if (schedule.DateTime.Date < today) continue;

                    var timeRange = "Время не указано";
                    if (_cachedShiftsDict != null && _cachedShiftsDict.TryGetValue(schedule.ShiftId, out var shift))
                    {
                        timeRange = $"{shift.StartAt:HH:mm} - {shift.EndAt:HH:mm}";
                    }

                    batchList.Add(new ScheduleUiModel(schedule, timeRange));
                }
            }

            if (absences != null)
            {
                batchList.AddRange(from absence in absences
                    let endDate =
                        absence.EndDate?.ToDateTime(TimeOnly.MaxValue) ??
                        absence.StartDate.ToDateTime(TimeOnly.MaxValue)
                    where endDate >= today
                    select absence);
            }

            var sortedBatch = batchList.OrderBy<object, object>(item =>
            {
                return item switch
                {
                    ScheduleUiModel s => s.DateTime,
                    AbsenceResponse a => a.StartDate.ToDateTime(TimeOnly.MinValue),
                    _ => DateTime.MaxValue
                };
            }).ToList();

            if (sortedBatch.Count > 0)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    foreach (var item in sortedBatch)
                    {
                        CalendarItems.Add(item);
                    }
                });
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"LoadDataInternal Error: {ex}");
        }
    }

    [RelayCommand]
    private static async Task GoBack() => await Shell.Current.GoToAsync("..");
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Work;
using Shared.Contracts.WorkProposal;
using Shared.Enums;
using Shared.Filters;
using System.Collections.ObjectModel;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.ViewModels;

public partial class AddProposalViewModel(
    WorkService workService,
    WorkProposalService proposalService,
    IdentityService identityService)
    : ObservableObject, IQueryAttributable
{
    private long _orderId;

    public ObservableCollection<WorkResponse> Jobs { get; } = [];

    [ObservableProperty] public partial WorkResponse? SelectedJob { get; set; }

    [ObservableProperty] public partial bool IsBusy { get; set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("OrderId", out var value))
        {
            return;
        }

        _orderId = Convert.ToInt64(value);
        LoadJobsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadJobs()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;

            var filter = new WorkFilter(
                SortBy: "title",
                Page: 1,
                Limit: 100,
                IsDescending: false
            );

            var (items, _) = await workService.GetWorks(filter);

            Jobs.Clear();
            if (items != null)
            {
                foreach (var item in items)
                {
                    Jobs.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading works: {ex}");
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось загрузить каталог работ", "ОК");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        if (SelectedJob == null)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Выберите работу", "ОК");
            return;
        }

        IsBusy = true;
        var (workerId, _) = await identityService.GetProfileIdAsync();

        var request = new WorkProposalRequest
        {
            OrderId = _orderId,
            JobId = SelectedJob.Id,
            WorkerId = (int)workerId,
            StatusId = ProposalStatusEnum.Pending,
            Date = DateTime.Now
        };

        var error = await proposalService.CreateWorkProposal(request);
        IsBusy = false;

        if (error == null)
        {
            await Shell.Current.DisplayAlert("Успех", "Предложение отправлено клиенту", "ОК");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", error, "ОК");
        }
    }

    [RelayCommand]
    public static async Task GoBack() => await Shell.Current.GoToAsync("..");
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Extentions;
using CRMSystemMobile.Services;
using Shared.Contracts.Order;
using Shared.Contracts.PartSet;
using Shared.Contracts.WorkInOrder;
using Shared.Contracts.WorkProposal;
using Shared.Enums;
using Shared.Filters;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class WorkerOrderDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly WorkInOrderService _workInOrderService;
    private readonly PartSetService _partSetService;
    private readonly WorkProposalService _workProposalService;
    private readonly IdentityService _identityService;

    public WorkerOrderDetailsViewModel(
        WorkInOrderService workInOrderService,
        PartSetService partSetService,
        WorkProposalService workProposalService,
        IdentityService identityService)
    {
        _workInOrderService = workInOrderService;
        _partSetService = partSetService;
        _workProposalService = workProposalService;
        _identityService = identityService;
    }

    [ObservableProperty]
    public partial OrderResponse Order { get; set; }

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }

    [ObservableProperty]
    public partial int SelectedTab { get; set; } = 0;

    public ObservableCollection<WorkInOrderResponse> MyWorks { get; } = [];
    public ObservableCollection<PartSetResponse> OrderParts { get; } = [];
    public ObservableCollection<WorkProposalResponse> MyProposals { get; } = [];

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Order"))
        {
            Order = (OrderResponse)query["Order"];
            LoadAllDataCommand.Execute(null);
        }
    }

    [RelayCommand]
    private async Task LoadAllData()
    {
        if (Order == null) return;
        IsBusy = true;

        try
        {
            await Task.WhenAll(
                LoadWorksInternal(),
                LoadPartsInternal(),
                LoadProposalsInternal()
            );
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось загрузить данные", "ОК");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    private async Task LoadWorksInternal()
    {
        var (profileId, _) = await _identityService.GetProfileIdAsync();
        var filter = new WorkInOrderFilter(
            OrderIds: [Order.Id],
            WorkerIds: [(int)profileId],
            JobIds: [],
            StatusIds: [],
            SortBy: null,
            Page: 1,
            Limit: 100,
            IsDescending: true
        );
        var (items, _) = await _workInOrderService.GetWorksInOrder(filter);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            MyWorks.Clear();
            if (items != null) foreach (var i in items) MyWorks.Add(i);
        });
    }

    private async Task LoadPartsInternal()
    {
        var filter = new PartSetFilter(
            OrderIds: [Order.Id],
            PositionIds: [],
            ProposalIds: [],
            SortBy: null,
            Page: 1,
            Limit: 100,
            IsDescending: true
        );
        var (items, _) = await _partSetService.GetPartSets(filter);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            OrderParts.Clear();
            if (items != null) foreach (var i in items) OrderParts.Add(i);
        });
    }

    private async Task LoadProposalsInternal()
    {
        var (profileId, _) = await _identityService.GetProfileIdAsync();
        var filter = new WorkProposalFilter(
            OrderIds: [Order.Id],
            WorkerIds: [(int)profileId],
            JobIds: [], StatusIds: [], SortBy: null, Page: 1, Limit: 100, IsDescending: true
        );
        var (items, _) = await _workProposalService.GetWorkProposals(filter);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            MyProposals.Clear();
            if (items != null) foreach (var i in items) MyProposals.Add(i);
        });
    }

    [RelayCommand]
    private void SelectTab(string tabIndex)
    {
        if (int.TryParse(tabIndex, out int index))
        {
            SelectedTab = index;
        }
    }

    [RelayCommand]
    private async Task GoToAddPart()
    {
        if (Order == null) return;
        var navParam = new Dictionary<string, object> { { "OrderId", Order.Id } };
        await Shell.Current.GoToAsync("AddPartPage", navParam);
    }

    [RelayCommand]
    private async Task GoToAddProposal()
    {
        if (Order == null) return;
        var navParam = new Dictionary<string, object> { { "OrderId", Order.Id } };
        await Shell.Current.GoToAsync("AddProposalPage", navParam);
    }

    [RelayCommand]
    private async Task ChangeStatus(WorkInOrderResponse work)
    {
        if (work == null) return;

        WorkStatusEnum newStatus;
        string actionName = "";

        if (work.StatusId == (int)WorkStatusEnum.Pending)
        {
            newStatus = WorkStatusEnum.InProgress;
            actionName = "начата";
        }
        else if (work.StatusId == (int)WorkStatusEnum.InProgress)
        {
            newStatus = WorkStatusEnum.Completed;
            actionName = "завершена";
        }
        else
        {
            return;
        }

        bool confirm = await Shell.Current.DisplayAlert("Подтверждение",
            $"Работа будет {actionName}. Продолжить?", "Да", "Нет");

        if (!confirm) return;

        var request = new WorkInOrderUpdateRequest
        {
            WorkerId = null,
            StatusId = newStatus,
            TimeSpent = null
        };

        var error = await _workInOrderService.UpdateWiO(work.Id, request);

        if (error == null)
        {
            await LoadAllData();
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", error, "ОК");
        }
    }

    [RelayCommand]
    private async Task GoBack() => await Shell.Current.GoToAsync("..");
}
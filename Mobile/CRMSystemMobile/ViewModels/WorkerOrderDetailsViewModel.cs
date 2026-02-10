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
    private readonly IdentityService _identityService;
    private readonly PartSetService _partService;         
    private readonly WorkProposalService _proposalService;

    public WorkerOrderDetailsViewModel(
        WorkInOrderService wioService,
        PartSetService partService,
        WorkProposalService proposalService)
    {
        _workInOrderService = wioService;
        _partService = partService;
        _proposalService = proposalService;
    }

    [ObservableProperty]
    public partial OrderResponse Order { get; set; }

    [ObservableProperty]
    public partial bool IsBusy { get; set; }
    public ObservableCollection<WorkInOrderResponse> Tasks { get; } = [];
    public ObservableCollection<PartSetResponse> Parts { get; } = [];
    public ObservableCollection<WorkProposalResponse> Proposals { get; } = [];

    public bool HasParts => Parts.Count > 0;
    public bool HasProposals => Proposals.Count > 0;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Order") && query["Order"] is OrderResponse order)
        {
            Order = order;
            LoadDataCommand.Execute(null);
        }
    }

    [RelayCommand]
    private async Task LoadData()
    {
        if (IsBusy || Order == null) return;
        IsBusy = true;

        try
        {
            var tasksTask = _workInOrderService.GetWorkInOrderByOrder(Order.Id);
            var partsTask = _partService.GetPartsByOrder(Order.Id);
            var proposalsTask = _proposalService.GetProposalsByOrder(Order.Id);

            await Task.WhenAll(tasksTask, partsTask, proposalsTask);

            var tasks = await tasksTask;
            var parts = await partsTask;
            var proposals = await proposalsTask;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Tasks.Clear();
                if (tasks != null) foreach (var t in tasks) Tasks.Add(t);

                Parts.Clear();
                if (parts != null) foreach (var p in parts) Parts.Add(p);

                Proposals.Clear();
                if (proposals != null) foreach (var p in proposals) Proposals.Add(p);

                // Обновляем видимость заголовков
                OnPropertyChanged(nameof(HasParts));
                OnPropertyChanged(nameof(HasProposals));
            });
        }
        finally
        {
            IsBusy = false;
        }
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

        var request = new WorkInOrderUpdateRequest { 
            WorkerId = null,
            StatusId = newStatus,
            TimeSpent = null
        };

        var error = await _workInOrderService.UpdateWiO(work.Id, request);

        if (error == null)
        {
            await LoadData(); 
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", error, "ОК");
        }
    }

    [RelayCommand]
    private async Task ShowAddMenu()
    {
        if (Order == null) return;

        string action = await Shell.Current.DisplayActionSheet("Добавить к заказу:", "Отмена", null, "Запчасть", "Предложить работу");

        var navParam = new Dictionary<string, object> { { "OrderId", Order.Id } };

        if (action == "Запчасть")
        {
            await Shell.Current.GoToAsync("AddPartPage", navParam);
        }
        else if (action == "Предложить работу")
        {
            await Shell.Current.GoToAsync("AddProposalPage", navParam);
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
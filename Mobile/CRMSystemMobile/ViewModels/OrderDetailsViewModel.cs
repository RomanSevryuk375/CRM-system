using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.PartSet;
using Shared.Contracts.WorkInOrder;
using Shared.Contracts.WorkProposal;
using Shared.Filters;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class OrderDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly WorkInOrderService _workInOrderService;
    private readonly PartSetService _partSetService;
    private readonly WorkProposalService _workProposalService;

    public OrderDetailsViewModel(
        WorkInOrderService workInOrderService,
        PartSetService partSetService,
        WorkProposalService workProposalService)
    {
        _workInOrderService = workInOrderService;
        _partSetService = partSetService;
        _workProposalService = workProposalService;
    }

    [ObservableProperty]
    public partial long OrderId { get; set; }

    [ObservableProperty]
    public partial string OrderTitle { get; set; }

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }

    public ObservableCollection<WorkInOrderResponse> Works { get; } = [];
    public ObservableCollection<PartSetResponse> Parts { get; } = [];
    public ObservableCollection<WorkProposalResponse> Proposals { get; } = [];

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("OrderId"))
        {
            OrderId = Convert.ToInt64(query["OrderId"]);
            OrderTitle = $"Заказ #{OrderId}";
            LoadDataCommand.Execute(null);
        }
    }

    [RelayCommand]
    private async Task LoadData()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Works.Clear();
            Parts.Clear();
            Proposals.Clear();

            var worksTask = LoadWorks();
            var partsTask = LoadParts();
            var proposalsTask = LoadProposals();

            await Task.WhenAll(worksTask, partsTask, proposalsTask);
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    private async Task LoadWorks()
    {
        var filter = new WorkInOrderFilter(
            OrderIds: [OrderId],
            JobIds: null,
            WorkerIds: null,
            StatusIds: null,
            SortBy: null,
            Page: 1,
            Limit: 100,
            IsDescending: true);

        var (items, _) = await _workInOrderService.GetWorksInOrder(filter);
        if (items != null)
        {
            foreach (var item in items) Works.Add(item);
        }
    }

    private async Task LoadParts()
    {
        var filter = new PartSetFilter(
            OrderIds: [OrderId],
            PositionIds: [], ProposalIds: [], SortBy: null, Page: 1, Limit: 100, IsDescending: true);

        var (items, _) = await _partSetService.GetPartSets(filter);
        if (items != null)
        {
            foreach (var item in items) Parts.Add(item);
        }
    }

    private async Task LoadProposals()
    {
        var filter = new WorkProposalFilter(
            OrderIds: [OrderId],
            JobIds: [], WorkerIds: [], StatusIds: [], SortBy: null, Page: 1, Limit: 100, IsDescending: true);

        var (items, _) = await _workProposalService.GetWorkProposals(filter);
        if (items != null)
        {
            foreach (var item in items) Proposals.Add(item);
        }
    }

    [RelayCommand]
    private async Task AcceptProposal(WorkProposalResponse proposal)
    {
        var error = await _workProposalService.AcceptWorkProposal(proposal.Id);
        if (error == null)
        {
            await Shell.Current.DisplayAlert("Успех", "Предложение принято", "ОК");
            await LoadData();
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", error, "ОК");
        }
    }

    [RelayCommand]
    private async Task RejectProposal(WorkProposalResponse proposal)
    {
        var error = await _workProposalService.RejectWorkProposal(proposal.Id);
        if (error == null)
        {
            await Shell.Current.DisplayAlert("Успех", "Предложение отклонено", "ОК");
            await LoadData();
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", error, "ОК");
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
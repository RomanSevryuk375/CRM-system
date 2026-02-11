using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CRMSystemMobile.Extentions;
using CRMSystemMobile.Message;
using CRMSystemMobile.Services;
using Shared.Contracts.Order;
using Shared.Filters;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class WorkerMainViewModel : ObservableObject, IRecipient<ProfileUpdatedMessage>
{
    private readonly OrderService _orderService;
    private readonly IdentityService _identityService;
    private readonly WorkerService _workerService;

    public WorkerMainViewModel(
        OrderService orderService,
        IdentityService identityService,
        WorkerService workerService)
    {
        _orderService = orderService;
        _identityService = identityService;
        _workerService = workerService;

        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    public ObservableCollection<OrderResponse> AssignedOrders { get; } = [];

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }

    [ObservableProperty]
    public partial bool IsMenuOpen { get; set; }

    [ObservableProperty]
    public partial string WorkerInitials { get; set; } = "??";

    private int _currentPage = 1;
    private int _totalItems = 0;
    private const int _pageSize = 20;

    public void Receive(ProfileUpdatedMessage message)
    {
        MainThread.BeginInvokeOnMainThread(async () => await LoadWorkerInfo());
    }

    [RelayCommand]
    private async Task LoadInitial()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;

            await LoadWorkerInfo();

            AssignedOrders.Clear();
            _currentPage = 1;

            await LoadDataInternal();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex}");
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
        if (IsBusy || (AssignedOrders.Count >= _totalItems && _totalItems != 0)) return;
        try
        {
            await LoadDataInternal();
        }
        catch { /* ... */ }
    }

    private async Task LoadDataInternal()
    {
        var (profileId, _) = await _identityService.GetProfileIdAsync();
        if (profileId <= 0) return;

        var filter = new OrderFilter(
            OrderIds: [],
            StatusIds: [1, 2, 5],
            PriorityIds: [],
            CarIds: [],
            ClientIds: [],
            WorkerIds: [(int)profileId],
            SortBy: "date",
            Page: _currentPage,
            Limit: _pageSize,
            IsDescending: true
        );

        var (items, total) = await _orderService.GetOrders(filter);
        _totalItems = total;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (items != null)
            {
                foreach (var item in items) AssignedOrders.Add(item);
            }
        });

        _currentPage++;
    }

    private async Task LoadWorkerInfo()
    {
        var (profileId, _) = await _identityService.GetProfileIdAsync();
        if (profileId > 0)
        {
            var worker = await _workerService.GetWorkerById((int)profileId);
            if (worker != null)
            {
                var s = worker.Surname?.FirstOrDefault().ToString() ?? "";
                var n = worker.Name?.FirstOrDefault().ToString() ?? "";
                WorkerInitials = (s + n).ToUpper();
            }
        }
    }

    [RelayCommand]
    private void ToggleMenu() => IsMenuOpen = !IsMenuOpen;

    [RelayCommand]
    private async Task Logout()
    {
        IsMenuOpen = false;
        SecureStorage.Default.Remove("jwt_token");
        await Shell.Current.GoToAsync("//LoginPage");
    }

    [RelayCommand]
    private async Task GoToWorkOrder(OrderResponse order)
    {
        if (order == null) return;

        var navParam = new Dictionary<string, object>
    {
        { "Order", order }
    };

        await Shell.Current.GoToAsync("WorkerOrderDetailsPage", navParam);
    }

    [RelayCommand]
    private async Task GoToProfile()
    {
        IsMenuOpen = false;
        await Shell.Current.GoToAsync("WorkerProfilePage");
    }

    [RelayCommand]
    private async Task GoToSchedule()
    {
        IsMenuOpen = false;
        await Shell.Current.GoToAsync("WorkerSchedulePage");
    }
}
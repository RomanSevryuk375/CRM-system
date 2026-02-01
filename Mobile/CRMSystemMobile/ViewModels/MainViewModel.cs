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

public partial class MainViewModel : ObservableObject
{
    private readonly OrderService _orderService;
    private readonly ClientService _clientService;
    private readonly IdentityService _identityService;

    public MainViewModel(OrderService orderService, ClientService clientService, IdentityService identityService)
    {
        _orderService = orderService;
        _clientService = clientService;
        _identityService = identityService;

        WeakReferenceMessenger.Default.RegisterAll(this);

        WeakReferenceMessenger.Default.Register<ProfileUpdatedMessage>(this, (r, m) =>
        {
            _ = LoadUserData();
            LoadInitialCommand.Execute(null);
        });

        LoadInitialCommand.Execute(null);
    }
    public ObservableCollection<OrderResponse> Orders { get; } = [];

    private int _currentPage = 1;
    private int _totalItems = 0;
    private const int _pageSize = 15;
    private int[] _activeStatuses = [1, 2, 3, 5];

    [ObservableProperty]
    public partial bool IsLoadingMore { get; set; }

    [ObservableProperty]
    public partial bool IsMenuSheetOpen { get; set; }

    [ObservableProperty]
    private string userInitials = "??";

    [RelayCommand]
    private void ToggleMenuSheet()
    {
        IsMenuSheetOpen = !IsMenuSheetOpen;
    }

    [RelayCommand]
    private async Task SelectTab(string tabName)
    {
        if (tabName == "InProgress")
        {
            _activeStatuses = [1, 2, 3, 5];
        }
        else
        {
            _activeStatuses = [4, 6];
        }

        await LoadInitial();
    }

    public async Task LoadUserData()
    {
        var (profileId, _) = await _identityService.GetProfileIdAsync();
        if (profileId > 0)
        {
            var client = await _clientService.GetClientById(profileId);
            if (client != null)
            {
                var s = client.Surname?.FirstOrDefault().ToString() ?? "";
                var n = client.Name?.FirstOrDefault().ToString() ?? "";

                if (string.IsNullOrEmpty(s) && string.IsNullOrEmpty(n))
                    UserInitials = "--";
                else
                    UserInitials = (s + n).ToUpper();
            }
            else
            {
                UserInitials = "X"; // No found client
            }
        }
        else
        {
            UserInitials = "?"; // don't get Id
        }
    }

    [RelayCommand]
    private async Task LoadInitial()
    {
        Orders.Clear();
        _currentPage = 1;
        _totalItems = 0;
        await LoadNextPage();
    }

    public void Receive(ProfileUpdatedMessage message)
    {
        MainThread.BeginInvokeOnMainThread(async () => await LoadUserData());
    }

    [RelayCommand]
    private async Task GoToProfile()
    {
        await Shell.Current.GoToAsync("ProfilePage");
    }

    [RelayCommand]
    private async Task NavigateTo(string route)
    {
        IsMenuSheetOpen = false;
        if (!string.IsNullOrEmpty(route))
        {
            await Shell.Current.GoToAsync(route);
        }
    }

    //[RelayCommand]
    //public void SetFilterStatusInWork()
    //{

    //}

    [RelayCommand]
    private async Task LoadNextPage()
    {
        if (IsLoadingMore || (Orders.Count >= _totalItems && _totalItems != 0))
            return;

        IsLoadingMore = true;

        try
        {
            var filter = new OrderFilter(
                OrderIds: [],
                StatusIds: _activeStatuses,
                PriorityIds: [],
                CarIds: [],
                ClientIds: [],
                WorkerIds: [],
                SortBy: null,
                Page: _currentPage,
                Limit: _pageSize,
                IsDescending: true
            );

            var (items, total) = await _orderService.GetOrders(filter);
            _totalItems = total;

            foreach (var item in items ?? [])
                Orders.Add(item);

            _currentPage++;
        }
        finally
        {
            IsLoadingMore = false;
        }
    }

    [RelayCommand]
    public async Task Logout()
    {
        bool answer = await Shell.Current.DisplayAlert("Выход", "Выйти из аккаунта?", "Да", "Нет");
        if (answer)
        {
            SecureStorage.Default.Remove("jwt_token");
            IsMenuSheetOpen = false;
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Order;
using Shared.Filters;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class MainViewModel(OrderService orderService) : ObservableObject
{
    public ObservableCollection<OrderResponse> Orders { get; } = [];

    private int _currentPage = 1;
    private int _totalItems = 0;
    private const int _pageSize = 15;
    private int[] _activeStatuses = [1, 2, 3, 5]; 

    [ObservableProperty]
    public partial bool IsLoadingMore { get; set; }

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

    [RelayCommand]
    private async Task LoadInitial()
    {
        Orders.Clear();
        _currentPage = 1;
        _totalItems = 0;
        await LoadNextPage();
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

            var (items, total) = await orderService.GetOrders(filter);
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

}

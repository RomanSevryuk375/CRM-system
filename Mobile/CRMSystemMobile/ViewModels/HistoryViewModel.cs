using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Order;
using Shared.Filters;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class HistoryViewModel(OrderService orderService) : ObservableObject
{
    public ObservableCollection<OrderResponse> Orders { get; } = [];

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }

    private readonly int[] _historyStatuses = [4, 6];

    [RelayCommand]
    private async Task LoadHistory()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var filter = new OrderFilter(
                OrderIds: [],
                StatusIds: _historyStatuses,
                PriorityIds: [],
                CarIds: [],
                ClientIds: [],
                WorkerIds: [],
                SortBy: "date",
                Page: 1,
                Limit: 100,
                IsDescending: true
            );

            var (items, _) = await orderService.GetOrders(filter);

            Orders.Clear();
            if (items != null)
            {
                foreach (var item in items)
                {
                    Orders.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось загрузить историю", "ОК");
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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Bill;
using Shared.Filters;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class BillsViewModel(BillService billService) : ObservableObject
{
    public ObservableCollection<BillResponse> Bills { get; } = [];

    private int _currentPage = 1;
    private int _totalItems = 0;
    private const int _pageSize = 15;

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsLoadingMore { get; set; } 

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }

    [RelayCommand]
    private async Task LoadInitial()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;
            Bills.Clear();
            if (items != null)
            {
                foreach (var item in items)
                {
                    Bills.Add(item);
                }
            }
        }

        try
        {
            IsLoadingMore = true;
            await LoadDataInternal();
        }
        finally
        {
            IsLoadingMore = false;
        }
    }

    private async Task LoadDataInternal()
    {
        var filter = new BillFilter(
             OrderIds: [],
             ClientIds: [],
             SortBy: "createdAt",
             Page: _currentPage,
             Limit: _pageSize,
             IsDescending: true
         );

        var (items, total) = await billService.GetBills(filter);
        _totalItems = total;

        if (items != null)
        {
            foreach (var item in items)
            {
                Bills.Add(item);
            }
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
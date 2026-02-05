using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.PaymentNote;
using Shared.Filters;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class PaymentsViewModel(PaymentService paymentService) : ObservableObject
{
    public ObservableCollection<PaymentNoteResponse> Payments { get; } = [];

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
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            Payments.Clear();
            _currentPage = 1;
            _totalItems = 0;
            await LoadDataInternal();
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось загрузить платежи", "ОК");
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
        if (IsLoadingMore || IsBusy || (Payments.Count >= _totalItems && _totalItems != 0))
        {
            return;
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
        var filter = new PaymentNoteFilter(
            BillIds: [],
            MethodIds: [],
            SortBy: "date",
            Page: _currentPage,
            Limit: _pageSize,
            IsDescending: true
        );

        var (items, total) = await paymentService.GetMyPayments(filter);
        _totalItems = total;

        if (items != null)
        {
            foreach (var item in items)
            {
                Payments.Add(item);
            }
        }
        _currentPage++;
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
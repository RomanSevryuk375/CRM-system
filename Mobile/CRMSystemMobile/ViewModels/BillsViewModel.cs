using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Bill;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class BillsViewModel(BillService billService) : ObservableObject
{
    public ObservableCollection<BillResponse> Bills { get; } = [];

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }

    [RelayCommand]
    private async Task LoadBills()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var items = await billService.GetMyBills();

            Bills.Clear();
            if (items != null)
            {
                foreach (var item in items)
                {
                    Bills.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось загрузить счета", "ОК");
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
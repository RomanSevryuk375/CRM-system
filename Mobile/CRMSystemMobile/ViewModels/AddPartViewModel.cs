using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.PartSet;
using Shared.Contracts.Position;
using Shared.Filters;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class AddPartViewModel(PositionService positionService, PartSetService partSetService)
    : ObservableObject, IQueryAttributable
{
    private long _orderId;

    public ObservableCollection<PositionResponse> Positions { get; } = [];

    [ObservableProperty] public partial PositionResponse? SelectedPosition { get; set; }

    [ObservableProperty] public partial decimal Quantity { get; set; } = 1;

    [ObservableProperty] public partial bool IsBusy { get; set; }

    [RelayCommand]
    private void IncreaseQuantity()
    {
        Quantity++;
    }

    [RelayCommand]
    private void DecreaseQuantity()
    {
        if (Quantity > 1)
        {
            Quantity--;
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("OrderId", out var value))
        {
            return;
        }

        _orderId = Convert.ToInt64(value);
        LoadPositionsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadPositions()
    {
        if (IsBusy)
        {
            return;
        }

        try
        {
            IsBusy = true;

            var filter = new PositionFilter(
                PartIds: [],
                SortBy: "part",
                Page: 1,
                Limit: 100,
                IsDescending: false
            );

            var (items, _) = await positionService.GetPositions(filter);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Positions.Clear();

                if (items == null)
                {
                    return;
                }

                foreach (var item in items)
                {
                    Positions.Add(item);
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading positions: {ex}");
            MainThread.BeginInvokeOnMainThread(() =>
                Shell.Current.DisplayAlert("Ошибка", "Не удалось загрузить список запчастей", "ОК"));
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        if (SelectedPosition == null)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Выберите запчасть", "ОК");
            return;
        }

        IsBusy = true;

        var request = new PartSetRequest
        {
            OrderId = _orderId,
            PositionId = SelectedPosition.Id,
            Quantity = Quantity,
            SoldPrice = SelectedPosition.SellingPrice
        };

        var error = await partSetService.AddToSet(request);

        IsBusy = false;

        if (error == null)
        {
            await Shell.Current.DisplayAlert("Успех", "Запчасть добавлена", "ОК");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", error, "ОК");
        }
    }

    [RelayCommand]
    private static async Task GoBack() => await Shell.Current.GoToAsync("..");
}
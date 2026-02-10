using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.PartSet;
using Shared.Contracts.Position;
using Shared.Filters;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class AddPartViewModel : ObservableObject, IQueryAttributable
{
    private readonly PositionService _positionService;
    private readonly PartSetService _partSetService;
    private long _orderId;

    public AddPartViewModel(PositionService positionService, PartSetService partSetService)
    {
        _positionService = positionService;
        _partSetService = partSetService;
    }

    public ObservableCollection<PositionResponse> Positions { get; } = [];

    [ObservableProperty]
    public partial PositionResponse SelectedPosition { get; set; }

    [ObservableProperty]
    public partial decimal Quantity { get; set; } = 1;

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("OrderId"))
        {
            _orderId = Convert.ToInt64(query["OrderId"]);
            LoadPositionsCommand.Execute(null);
        }
    }

    [RelayCommand]
    private async Task LoadPositions()
    {
        if (IsBusy) return;

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

            var (items, totalCount) = await _positionService.GetPositions(filter);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Positions.Clear();

                if (items != null)
                {
                    foreach (var item in items)
                    {
                        Positions.Add(item);
                    }
                }
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading positions: {ex}");
            MainThread.BeginInvokeOnMainThread(async () =>
                await Shell.Current.DisplayAlert("Ошибка", "Не удалось загрузить список запчастей", "ОК"));
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

        var error = await _partSetService.AddToSet(request);

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
    private async Task GoBack() => await Shell.Current.GoToAsync("..");
}
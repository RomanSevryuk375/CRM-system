using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Contracts.Car;

namespace CRMSystemMobile.ViewModels;

public partial class CarDetailsViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private CarResponse _car;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Car") && query["Car"] is CarResponse carData)
        {
            Car = carData;
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task BookVisit()
    {
        // Логика перехода на страницу бронирования (календарь)
        await Shell.Current.DisplayAlert("Бронирование", "Переход на экран выбора даты", "OK");
    }
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Contracts.Car;

namespace CRMSystemMobile.ViewModels;

public partial class CarDetailsViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] public partial CarResponse? Car { get; set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Car", out var value))
        {
            Car = (CarResponse)value;
        }
    }

    [RelayCommand]
    private async Task BookVisit()
    {
        var navigationParameter = new Dictionary<string, object>
        {
            { "SelectedCar", Car }
        };

        await Shell.Current.GoToAsync("BookingPage", navigationParameter);
    }

    [RelayCommand]
    private static async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
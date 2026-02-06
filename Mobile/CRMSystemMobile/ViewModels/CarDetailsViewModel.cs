using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Shared.Contracts.Car;

namespace CRMSystemMobile.ViewModels;

public partial class CarDetailsViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private CarResponse car;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Car", out object? value))
        {
            Car = (CarResponse)value;
        }
    }

    [RelayCommand]
    private async Task BookVisit()
    {
        if (Car == null)
        {
            return;
        }

        var navigationParameter = new Dictionary<string, object>
        {
            { "SelectedCar", Car }
        };

        await Shell.Current.GoToAsync("BookingPage", navigationParameter);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
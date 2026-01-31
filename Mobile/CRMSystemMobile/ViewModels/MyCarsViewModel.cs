using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Car;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class MyCarsViewModel(CarService carService) : ObservableObject
{
    public ObservableCollection<CarResponse> Cars { get; } = [];

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }

    [RelayCommand]
    private async Task LoadCars()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var carsList = await carService.GetMyCars();

            Cars.Clear();
            if (carsList != null)
            {
                foreach (var car in carsList)
                {
                    Cars.Add(car);
                }
            }
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
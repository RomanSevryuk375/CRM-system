using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Car;
using Shared.Filters;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class MyCarsViewModel(CarService carService) : ObservableObject
{
    public ObservableCollection<CarResponse> Cars { get; } = [];

    private int _currentPage = 1;
    private int _totalItems = 0;
    private const int _pageSize = 15;

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool IsLoadingMore { get; set; }

    [ObservableProperty]
    public partial bool IsRefreshing { get; set; }

    [ObservableProperty]
    public partial bool IsMenuOpen { get; set; } = false;

    [RelayCommand]
    private async Task LoadInitial()
    {
        if (IsBusy) 
        { 
            return; 
        }

        // Временно убираем проверку IsBusy, чтобы исключить "залипание" флага
        // if (IsBusy) { System.Diagnostics.Debug.WriteLine("DEBUG: IsBusy = true, выход"); return; }

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
        _currentPage++;
    }

    [RelayCommand]
    private async Task GoToAddCar()
    {
        await Shell.Current.GoToAsync("AddCarPage");
    }

    [RelayCommand]
    private async Task GoBack()
    {
        if (IsMenuOpen)
        {
            IsMenuOpen = false;
            return;
        }
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private void ToggleMenu()
    {
        IsMenuOpen = !IsMenuOpen;
    }

    [RelayCommand]
    private async Task GoToDetails(CarResponse car)
    {
        if (car == null)
        {
            return;
        }

        var navigationParameter = new Dictionary<string, object>
        {
            { "Car", car }
        };
        await Shell.Current.GoToAsync("CarDetailsPage", navigationParameter);
    }

    [RelayCommand]
    private async Task NavigateTo(string page)
    {
        IsMenuOpen = false;

        if (page == "MyCarsPage")
        {
            return;
        }

        await Shell.Current.GoToAsync(page);
    }

    [RelayCommand]
    private async Task Logout()
    {
        IsMenuOpen = false;
        SecureStorage.Default.Remove("jwt_token");
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
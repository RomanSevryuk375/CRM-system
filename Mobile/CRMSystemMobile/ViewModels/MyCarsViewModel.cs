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

        try
        {
            IsBusy = true;
            Cars.Clear();
            _currentPage = 1;
            _totalItems = 0;

            await LoadDataInternal();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка", $"Не удалось загрузить авто: {ex}", "ОК");
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
        if (IsLoadingMore || IsBusy || (Cars.Count >= _totalItems && _totalItems != 0))
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
        var filter = new CarFilter(
            OwnerIds: [],
            SortBy: null,
            Page: _currentPage,
            Limit: _pageSize,
            IsDescending: true
        );
        
        var (items, total) = await carService.GetCars(filter);

        _totalItems = total;

        if (items != null)
        {
            foreach (var item in items)
            {
                Cars.Add(item);
            }
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
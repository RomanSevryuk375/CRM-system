using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Car;
using Shared.Contracts.Order;
using Shared.Enums;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class BookingViewModel(OrderService orderService, CarService carService) : ObservableObject, IQueryAttributable
{
    private CarResponse _preSelectedCar;
    public ObservableCollection<CarResponse> MyCars { get; } = [];

    [ObservableProperty]
    public partial CarResponse SelectedCar { get; set; }

    [ObservableProperty]
    public partial DateTime SelectedDate { get; set; } = DateTime.Now.AddDays(1);

    [ObservableProperty]
    public partial DateTime MinDate { get; set; } = DateTime.Now;

    public List<string> Priorities { get; } = ["Низкий", "Обычный", "Высокий"];

    [ObservableProperty]
    public partial string SelectedPriorityName { get; set; } = "Обычный";

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("SelectedCar", out object? value))
        {
            _preSelectedCar = (CarResponse)value;
        }
    }

    [RelayCommand]
    private async Task LoadData()
    {
        var cars = await carService.GetMyCars();
        MyCars.Clear();

        if (cars != null && cars.Count != 0)
        {
            foreach (var car in cars)
            {
                MyCars.Add(car);
            }

            if (_preSelectedCar != null)
            {
                SelectedCar = MyCars.FirstOrDefault(c => c.Id == _preSelectedCar.Id);
                _preSelectedCar = null;
            }
            else
            {
                SelectedCar = MyCars.FirstOrDefault();
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Внимание", "Добавьте авто", "ОК");
            await Shell.Current.GoToAsync("AddCarPage");
        }
    }

    [RelayCommand]
    private async Task BookAppointment()
    {
        if (SelectedCar == null)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Выберите автомобиль", "ОК");
            return;
        }

        var priorityEnum = SelectedPriorityName switch
        {
            "Низкий" => OrderPriorityEnum.Low,
            "Обычный" => OrderPriorityEnum.Medium,
            "Высокий" => OrderPriorityEnum.High,
            _ => OrderPriorityEnum.Medium
        };

        var request = new OrderRequest
        {
            CarId = SelectedCar.Id,
            StatusId = OrderStatusEnum.Pending,
            PriorityId = priorityEnum,
            Date = DateOnly.FromDateTime(SelectedDate)
        };

        var error = await orderService.CreateOrder(request);

        if (error == null)
        {
            await Shell.Current.DisplayAlert("Успех", "Заявка отправлена! Менеджер свяжется с вами.", "ОК");
            await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            await Shell.Current.DisplayAlert("Не удалось записаться", error, "ОК");
        }
    }

    [RelayCommand]
    private async Task GoBack() => await Shell.Current.GoToAsync("..");
}
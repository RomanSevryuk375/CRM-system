using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Car;
using Shared.Contracts.Order;
using Shared.Enums;

namespace CRMSystemMobile.ViewModels;

public partial class BookingViewModel(OrderService orderService)
    : ObservableObject, IQueryAttributable
{
    // Машина, которую передали с предыдущей страницы
    [ObservableProperty]
    public partial CarResponse SelectedCar { get; set; }

    [ObservableProperty]
    public partial DateTime SelectedDate { get; set; } = DateTime.Now.AddDays(1);

    [ObservableProperty]
    public partial DateTime MinDate { get; set; } = DateTime.Now;

    [ObservableProperty]
    public partial string Description { get; set; }
    public List<string> Priorities { get; } = ["Низкий", "Обычный", "Высокий"];

    [ObservableProperty]
    public partial string SelectedPriorityName { get; set; } = "Обычный";

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("SelectedCar"))
        {
            SelectedCar = (CarResponse)query["SelectedCar"];
        }
    }

    [RelayCommand]
    private async Task BookAppointment()
    {
        if (SelectedCar == null)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Автомобиль не выбран", "ОК");
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
            Date = DateOnly.FromDateTime(SelectedDate),
            // Description = Description 
        };

        var error = await orderService.CreateOrder(request);

        if (error == null)
        {
            await Shell.Current.DisplayAlert("Успех", "Заявка отправлена!", "ОК");
            await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", error, "ОК");
        }
    }

    [RelayCommand]
    private async Task GoBack() => await Shell.Current.GoToAsync("..");
}
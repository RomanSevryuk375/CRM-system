using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Car;
using Shared.Contracts.Order;
using Shared.Enums;

namespace CRMSystemMobile.ViewModels;

public partial class BookingViewModel(OrderService orderService) : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    public partial CarResponse Car { get; set; }

    [ObservableProperty]
    public partial DateTime SelectedDate { get; set; } = DateTime.Now.AddDays(1);

    [ObservableProperty]
    public partial string ComplaintDescription { get; set; }

    public List<string> Priorities { get; } = ["Низкий", "Обычный", "Высокий"];

    [ObservableProperty]
    public partial string SelectedPriorityName { get; set; } = "Обычный";

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Car") && query["Car"] is CarResponse carData)
        {
            Car = carData;
        }
    }

    [RelayCommand]
    private async Task GoBack() => await Shell.Current.GoToAsync("..");

    [RelayCommand]
    private async Task SubmitOrder()
    {
        if (IsBusy)
        {
            return;
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
                CarId = Car.Id,
                Date = DateOnly.FromDateTime(SelectedDate),
                PriorityId = priorityEnum,
                StatusId = OrderStatusEnum.Pending
                // Description = ComplaintDescription
            };

        var error = await orderService.CreateOrder(request);

            if (success)
            {
                await Shell.Current.DisplayAlert("Успех", "Вы записаны на сервис!", "OK");
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка", "Не удалось создать запись. Попробуйте позже.", "OK");
            }
        }
        finally
        {
            IsBusy = false;
        }
    }
}
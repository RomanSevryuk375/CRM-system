using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Extentions;
using CRMSystemMobile.Services;
using Shared.Contracts.Car;
using Shared.Enums;
using System.Text.RegularExpressions;

namespace CRMSystemMobile.ViewModels;

public partial class AddCarViewModel(CarService carService, IdentityService identityService) : ObservableObject
{
    [ObservableProperty]
    public partial string Brand { get; set; }

    [ObservableProperty]
    public partial string Model { get; set; }

    [ObservableProperty]
    public partial int Year { get; set; } = DateTime.Now.Year;

    [ObservableProperty]
    public partial string VinNumber { get; set; }

    [ObservableProperty]
    public partial string StateNumber { get; set; }

    [ObservableProperty]
    public partial int Mileage { get; set; }

    [RelayCommand]
    private async Task SaveCar()
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(Brand))
        {
            errors.Add("Марка не может быть пустой.");
        }
        else if (Brand.Length > 128)
        {
            errors.Add("Название марки слишком длинное.");
        }

        if (string.IsNullOrWhiteSpace(Model))
        {
            errors.Add("Модель не может быть пустой.");
        }
        else if (Model.Length > 256)
        {
            errors.Add("Название модели слишком длинное.");
        }

        if (Year < 1900)
        {
            errors.Add("Мы не ремонтируем старый хлам (год < 1900).");
        }

        if (Year > DateTime.Now.Year + 1)
        {
            errors.Add("Год выпуска не может быть в будущем.");
        }

        var vinPattern = @"^[A-HJ-NPR-Z0-9]{17}$";
        if (string.IsNullOrWhiteSpace(VinNumber))
        {
            errors.Add("VIN номер обязателен.");
        }
        else if (!Regex.IsMatch(VinNumber.ToUpper(), vinPattern))
        {
            errors.Add("Некорректный формат VIN (нужно 17 символов, латиница без I, O, Q и цифры).");
        }

        var stateNumPattern = @"^(\d{4}\s?[ABEIKMHOPCTX]{2}-[1-7]|[ABEIKMHOPCTX]{2}\s?\d{4}-[1-7]|(TA|TT|TY)\d{4}|E\d{3}[ABEIKMHOPCTX]{2}[1-7])$";

        if (string.IsNullOrWhiteSpace(StateNumber))
        {
            errors.Add("Гос. номер обязателен.");
        }
        else if (!Regex.IsMatch(StateNumber.ToUpper(), stateNumPattern))
        {
            errors.Add("Некорректный формат гос. номера (пример: 1234 AB-7).");
        }

        if (Mileage < 0)
        {
            errors.Add("Пробег не может быть отрицательным.");
        }

        if (errors.Count != 0)
        {
            var message = string.Join("\n", errors);
            await Shell.Current.DisplayAlert("Ошибка валидации", message, "ОК");
            return;
        }

        var (profileId, _) = await identityService.GetProfileIdAsync();

        var request = new CarRequest
        {
            OwnerId = profileId,
            StatusId = (int)CarStatusEnum.NotAtWork,
            Brand = Brand,
            Model = Model,
            YearOfManufacture = Year,
            VinNumber = VinNumber.ToUpper(),
            StateNumber = StateNumber.ToUpper(),
            Mileage = Mileage
        };

        var errorResponse = await carService.CreateCar(request);

        if (errorResponse == null)
        {
            await Shell.Current.DisplayAlert("Успех", "Автомобиль добавлен", "ОК");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка сервера", errorResponse, "ОК");
        }
    }

    [RelayCommand]
    private async Task GoBack() => await Shell.Current.GoToAsync("..");
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Acceptance;
using Shared.Contracts.Order;
using System.Collections.ObjectModel;
using CRMSystemMobile.Extensions;

namespace CRMSystemMobile.ViewModels;

public partial class OrderAcceptanceViewModel(
    AcceptanceService acceptanceService,
    AcceptanceImgService imgService,
    IdentityService identityService)
    : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    public partial OrderResponse Order { get; set; }

    [ObservableProperty]
    public partial long? AcceptanceId { get; set; }

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial string Mileage { get; set; }

    [ObservableProperty]
    public partial double FuelLevel { get; set; }

    [ObservableProperty]
    public partial string ExternalDefects { get; set; }

    [ObservableProperty]
    public partial string InternalDefects { get; set; }

    public ObservableCollection<AcceptancePhotoUiModel> Photos { get; } = [];

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("Order", out var value) || value is not OrderResponse order)
        {
            return;
        }

        Order = order;
        LoadDataCommand.Execute(null);
    }

    [RelayCommand]
    public async Task LoadData()
    {
        if (IsBusy)
        {
            return;
        }

        IsBusy = true;

        try
        {
            var acceptance = await acceptanceService.GetAcceptanceByOrder(Order.Id);

            if (acceptance != null)
            {
                AcceptanceId = acceptance.Id;
                Mileage = acceptance.Mileage.ToString();
                FuelLevel = acceptance.FuelLevel / 100.0; 
                ExternalDefects = acceptance.ExternalDefects ?? "";
                InternalDefects = acceptance.InternalDefects ?? "";

                await LoadPhotos(acceptance.Id);
            }
        }
        finally { IsBusy = false; }
    }

    public async Task LoadPhotos(long acceptanceId)
    {
        var images = await imgService.GetPhotos(acceptanceId);
        Photos.Clear();
        if (images != null)
        {
            foreach (var img in images)
            {
                var bytes = await imgService.DownloadPhotoBytes(img.Id);
                if (bytes != null)
                {
                    Photos.Add(new AcceptancePhotoUiModel 
                    { 
                        Id = img.Id, 
                        ImageSource = ImageSource.FromStream(() => new MemoryStream(bytes)) 
                    });
                }
            }
        }
    }

    [RelayCommand]
    private async Task SaveChanges()
    {
        if (IsBusy)
        {
            return;
        }

        if (!int.TryParse(Mileage, out var mileageInt) || mileageInt <= 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Пробег должен быть больше 0", "ОК");
            return;
        }

        var fuelLevelInt = (int)(FuelLevel * 100);
        if (fuelLevelInt <= 0)
        {
            fuelLevelInt = 1;
        }

        IsBusy = true;
        try
        {
            var (workerId, _) = await identityService.GetProfileIdAsync();

            if (workerId <= 0)
            {
                await Shell.Current.DisplayAlert("Ошибка", "Не удалось определить ID сотрудника. Перезайдите в приложение.", "ОК");
                return;
            }

            var request = new AcceptanceRequest
            {
                OrderId = Order.Id,
                WorkerId = (int)workerId,
                CreatedAt = DateTime.Now.AddMinutes(-1),
                Mileage = mileageInt,
                FuelLevel = fuelLevelInt,
                ExternalDefects = string.IsNullOrEmpty(ExternalDefects) ? "Нет" : ExternalDefects,
                InternalDefects = string.IsNullOrEmpty(InternalDefects) ? "Нет" : InternalDefects,
                ClientSign = false,
                WorkerSign = true
            };

            var (id, error) = await acceptanceService.CreateOrUpdateAcceptance(request, AcceptanceId);

            if (error == null && id.HasValue)
            {
                AcceptanceId = id.Value;
                await Shell.Current.DisplayAlert("Успех", "Данные сохранены", "ОК");
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка сервера", error, "ОК");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Исключение", ex.Message, "ОК");
        }
        finally { IsBusy = false; }
    }

    [RelayCommand]
    private async Task PickPhoto()
    {
        if (AcceptanceId == null)
        {
            await Shell.Current.DisplayAlert("Внимание", "Сначала сохраните основные данные", "ОК");
            return;
        }

        try
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();
            
            await UploadPhotoInternal(photo);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка галереи", ex.Message, "ОК");
        }
    }

    [RelayCommand]
    private async Task TakePhoto()
    {
        if (AcceptanceId == null)
        {
            await Shell.Current.DisplayAlert("Внимание", "Сначала сохраните основные данные", "ОК");
            return;
        }

        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                var photo = await MediaPicker.Default.CapturePhotoAsync();
                
                await UploadPhotoInternal(photo);
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка", "Камера не поддерживается", "ОК");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Ошибка камеры", ex.Message, "ОК");
        }
    }

    private async Task UploadPhotoInternal(FileResult? photo)
    {
        if (photo == null)
        {
            return;
        }

        IsBusy = true;
        try
        {
            if (AcceptanceId == null)
            {
                return;
            }

            var error = await imgService.UploadPhoto(AcceptanceId.Value, photo);

            if (error == null)
            {
                await LoadPhotos(AcceptanceId.Value); 
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка загрузки", error, "ОК");
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private static async Task GoBack() => await Shell.Current.GoToAsync("..");
}
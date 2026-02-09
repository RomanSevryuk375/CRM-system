using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CRMSystemMobile.Extentions;
using CRMSystemMobile.Message;
using CRMSystemMobile.Services;
using Shared.Contracts.Worker;

namespace CRMSystemMobile.ViewModels;

public partial class WorkerProfileViewModel : ObservableObject
{
    private readonly WorkerService _workerService;
    private readonly IdentityService _identityService;
    private int _currentWorkerId;

    public WorkerProfileViewModel(WorkerService workerService, IdentityService identityService)
    {
        _workerService = workerService;
        _identityService = identityService;
        LoadProfileCommand.Execute(null);
    }

    [ObservableProperty]
    public partial string Name { get; set; }

    [ObservableProperty]
    public partial string Surname { get; set; }

    [ObservableProperty]
    public partial string PhoneNumber { get; set; }

    [ObservableProperty]
    public partial string Email { get; set; }

    [ObservableProperty]
    public partial decimal HourlyRate { get; set; }

    [ObservableProperty]
    public partial bool IsLoading { get; set; }

    public string Initials => $"{Surname?.FirstOrDefault()}{Name?.FirstOrDefault()}".ToUpper();

    [RelayCommand]
    public async Task LoadProfile()
    {
        IsLoading = true;
        var (profileId, _) = await _identityService.GetProfileIdAsync();
        _currentWorkerId = (int)profileId;

        if (_currentWorkerId > 0)
        {
            var worker = await _workerService.GetWorkerById(_currentWorkerId);
            if (worker != null)
            {
                Name = worker.Name;
                Surname = worker.Surname;
                PhoneNumber = worker.PhoneNumber;
                Email = worker.Email;
                HourlyRate = worker.HourlyRate;
                OnPropertyChanged(nameof(Initials));
            }
        }
        IsLoading = false;
    }

    [RelayCommand]
    public async Task SaveProfile()
    {
        if (IsLoading) return;
        IsLoading = true;

        var request = new WorkerUpdateRequest
        {
            Name = Name,
            Surname = Surname,
            PhoneNumber = PhoneNumber,
            Email = Email,
            HourlyRate = null
        };

        var success = await _workerService.UpdateWorker(_currentWorkerId, request);

        if (success)
        {
            await Shell.Current.DisplayAlert("Успех", "Данные обновлены", "ОК");
            WeakReferenceMessenger.Default.Send(new ProfileUpdatedMessage("WorkerUpdated"));
            OnPropertyChanged(nameof(Initials));
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось сохранить", "ОК");
        }
        IsLoading = false;
    }

    [RelayCommand]
    public async Task Logout()
    {
        if (await Shell.Current.DisplayAlert("Выход", "Выйти?", "Да", "Нет"))
        {
            SecureStorage.Default.Remove("jwt_token");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }

    [RelayCommand]
    private async Task GoBack() => await Shell.Current.GoToAsync("..");
}
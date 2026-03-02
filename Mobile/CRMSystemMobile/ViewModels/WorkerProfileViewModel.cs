using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CRMSystemMobile.Extensions;
using CRMSystemMobile.Message;
using CRMSystemMobile.Services;
using Shared.Contracts.Skill;
using Shared.Contracts.Specialization;
using Shared.Contracts.Worker;

namespace CRMSystemMobile.ViewModels;

public partial class WorkerProfileViewModel : ObservableObject
{
    private readonly WorkerService _workerService;
    private readonly IdentityService _identityService;
    private readonly SkillService _skillService;
    private readonly SpecializationService _specService;
    private int _currentWorkerId;

    public WorkerProfileViewModel(SkillService skillService,
        SpecializationService specService, WorkerService workerService, IdentityService identityService)
    {
        _skillService = skillService;
        _specService = specService;
        _workerService = workerService;
        _identityService = identityService;
        LoadProfileCommand.Execute(null);
    }

    [ObservableProperty] public partial string? Name { get; set; }

    [ObservableProperty] public partial string? Surname { get; set; }

    [ObservableProperty] public partial string? PhoneNumber { get; set; }

    [ObservableProperty] public partial string? Email { get; set; }

    [ObservableProperty] public partial decimal HourlyRate { get; set; }

    [ObservableProperty] public partial bool IsLoading { get; set; }

    public string Initials =>
        $"{(Surname ?? string.Empty).FirstOrDefault()}{(Name ?? string.Empty).FirstOrDefault()}".ToUpper();

    public ObservableCollection<SkillResponse> MySkills { get; } = [];

    public ObservableCollection<SpecializationResponse> AvailableSpecializations { get; } = [];

    [ObservableProperty] public partial SpecializationResponse? SelectedSpecialization { get; set; }

    [RelayCommand]
    public async Task LoadProfile()
    {
        if (IsLoading)
        {
            return;
        }

        IsLoading = true;
        var (profileId, _) = await _identityService.GetProfileIdAsync();
        if (profileId <= 0)
        {
            return;
        }

        _currentWorkerId = (int)profileId;

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

        var skills = await _skillService.GetWorkerSkills((int)profileId);
        var specs = await _specService.GetAllSpecializations();

        MySkills.Clear();
        if (skills != null)
        {
            foreach (var s in skills)
            {
                MySkills.Add(s);
            }
        }

        AvailableSpecializations.Clear();
        if (specs != null)
        {
            foreach (var s in specs)
            {
                AvailableSpecializations.Add(s);
            }
        }

        IsLoading = false;
    }

    [RelayCommand]
    public async Task SaveProfile()
    {
        if (IsLoading)
        {
            return;
        }

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
    public static async Task Logout()
    {
        if (await Shell.Current.DisplayAlert("Выход", "Выйти?", "Да", "Нет"))
        {
            SecureStorage.Default.Remove("jwt_token");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }

    [RelayCommand]
    private static async Task GoBack() => await Shell.Current.GoToAsync("..");
}
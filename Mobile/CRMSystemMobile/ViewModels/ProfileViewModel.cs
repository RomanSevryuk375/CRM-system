using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CRMSystemMobile.Extentions;
using CRMSystemMobile.Message;
using CRMSystemMobile.Services;
using Shared.Contracts.Client;

namespace CRMSystemMobile.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    private readonly ClientService _clientService;
    private readonly IdentityService _identityService;
    private long _currentClientId;

    public ProfileViewModel(ClientService clientService, IdentityService identityService)
    {
        _clientService = clientService;
        _identityService = identityService;
        LoadProfileCommand.Execute(null);
    }

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string surname;

    [ObservableProperty]
    private string phoneNumber;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private bool isLoading;

    public string Initials => $"{Surname?.FirstOrDefault()}{Name?.FirstOrDefault()}".ToUpper();

    [RelayCommand]
    public async Task LoadProfile()
    {
        IsLoading = true;
        var (profileId, roleId) = await _identityService.GetProfileIdAsync();
        _currentClientId = profileId;

        if (profileId > 0)
        {
            var client = await _clientService.GetClientById(profileId);
            if (client != null)
            {
                Name = client.Name;
                Surname = client.Surname;
                PhoneNumber = client.PhoneNumber;
                Email = client.Email;

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

        var request = new ClientUpdateRequest(
            Name,
            Surname,
            PhoneNumber,
            Email
        );

        var success = await _clientService.UpdateClient(_currentClientId, request);

        if (success)
        {
            await Shell.Current.DisplayAlert("Успех", "Данные обновлены", "ОК");

            WeakReferenceMessenger.Default.Send(new ProfileUpdatedMessage("Updated"));

            OnPropertyChanged(nameof(Initials));
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось сохранить данные", "ОК");
        }

        IsLoading = false;
    }

    [RelayCommand]
    public async Task Logout()
    {
        bool answer = await Shell.Current.DisplayAlert("Выход", "Выйти из аккаунта?", "Да", "Нет");
        if (answer)
        {
            SecureStorage.Default.Remove("jwt_token");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }

    [RelayCommand]
    public async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CRMSystemMobile.Extensions;
using CRMSystemMobile.Message;
using CRMSystemMobile.Services;
using Shared.Contracts.Client;

namespace CRMSystemMobile.ViewModels;

public partial class ProfileViewModel(ClientService clientService, IdentityService identityService)
    : ObservableObject
{
    private long _currentClientId;

    [ObservableProperty] public partial string? Name { get; set; }

    [ObservableProperty] public partial string? Surname { get; set; }

    [ObservableProperty] public partial string? PhoneNumber { get; set; }

    [ObservableProperty] public partial string? Email { get; set; }

    [ObservableProperty] public partial bool IsLoading { get; set; }

    public string Initials => $"{Surname?.FirstOrDefault()}{Name?.FirstOrDefault()}".ToUpper();

    [RelayCommand]
    public async Task LoadProfile()
    {
        IsLoading = true;
        var (profileId, _) = await identityService.GetProfileIdAsync();
        _currentClientId = profileId;

        if (profileId > 0)
        {
            var client = await clientService.GetClientById(profileId);
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
        if (IsLoading)
        {
            return;
        }

        IsLoading = true;

        var request = new ClientUpdateRequest
        {
            Name = Name,
            Surname = Surname,
            PhoneNumber = PhoneNumber,
            Email = Email,
        };

        var success = await clientService.UpdateClient(_currentClientId, request);

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
        var answer = await Shell.Current.DisplayAlert("Выход", "Выйти из аккаунта?", "Да", "Нет");
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
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Client;
using Shared.Contracts.Login;
using Shared.Enums;
using System.Collections;
using System.ComponentModel;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace CRMSystemMobile.ViewModels;

public partial class RegistrationViewModel : ObservableObject, INotifyDataErrorInfo
{

    private readonly Dictionary<string, List<string>> _errors = new();
    private readonly RegistrationService registrationService;
    private readonly LoginService loginService;

    public RegistrationViewModel(
        RegistrationService registrationService,
        LoginService loginService)
    {
        this.registrationService = registrationService;
        this.loginService = loginService;
    }

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public bool HasErrors => _errors.Any(kv => kv.Value?.Count > 0);

    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return _errors.SelectMany(kv => kv.Value).ToList();
        return _errors.TryGetValue(propertyName, out var list) ? list : new List<string>();
    }

    private void AddError(string propertyName, string error)
    {
        if (!_errors.TryGetValue(propertyName, out var list))
        {
            list = new List<string>();
            _errors[propertyName] = list;
        }
        if (!list.Contains(error))
        {
            list.Add(error);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }
    }

    private void ClearErrors(string propertyName)
    {
        if (_errors.Remove(propertyName))
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }
    }

    [ObservableProperty]
    public partial string NameSurnameError { get; set; }

    [ObservableProperty]
    public partial string ClientPhoneNumberError { get; set; }

    [ObservableProperty]
    public partial string ClientEmailError { get; set; }

    [ObservableProperty]
    public partial string UserLoginError { get; set; }

    [ObservableProperty]
    public partial string UserPasswordError { get; set; }

    [ObservableProperty]
    public partial bool IsPasswordHidden { get; set; } = true;

    [ObservableProperty]
    public partial string PasswordIcon { get; set; } = "eye_hide.png";

    [RelayCommand]
    private void TogglePassword()
    {
        IsPasswordHidden = !IsPasswordHidden;
        PasswordIcon = IsPasswordHidden ? "eye_hide.png" : "visible_hide.png";
    }

    private void UpdateErrorString(string propertyName)
    {
        var errors = GetErrors(propertyName).OfType<string>();
        var combined = string.Join(Environment.NewLine, errors);

        switch (propertyName)
        {
            case nameof(NameSurname):
                NameSurnameError = combined;
                break;
            case nameof(ClientPhoneNumber):
                ClientPhoneNumberError = combined;
                break;
            case nameof(ClientEmail):
                ClientEmailError = combined;
                break;
            case nameof(UserLogin):
                UserLoginError = combined;
                break;
            case nameof(UserPassword):
                UserPasswordError = combined;
                break;
        }
    }

    private void ValidateProperty(string propertyName, string? value)
    {
        ClearErrors(propertyName);
        switch (propertyName)
        {
            case nameof(NameSurname):
                if (string.IsNullOrWhiteSpace(value))
                {
                    AddError(propertyName, "Введите фамилию и имя.");
                }
                else
                {
                    var parts = value.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 2)
                    {
                        AddError(propertyName, "Укажите фамилию и имя через пробел.");
                    }
                }
                break;

            case nameof(ClientPhoneNumber):
                if (string.IsNullOrWhiteSpace(value))
                {
                    AddError(propertyName, "Введите номер телефона.");
                }
                else
                {
                    var phoneRegex = MyRegex;
                    if (!phoneRegex.IsMatch(value.Trim()))
                        AddError(propertyName, "Неверный формат телефона. Пример: (+375/80)(29/44/33/25)XXX-XX-XX");
                }
                break;

            case nameof(ClientEmail):
                if (!string.IsNullOrWhiteSpace(value))
                {
                    try
                    {
                        _ = new MailAddress(value);
                    }
                    catch
                    {
                        AddError(propertyName, "Неверный формат e-mail.");
                    }
                }
                break;

            case nameof(UserLogin):
                if (string.IsNullOrWhiteSpace(value))
                    AddError(propertyName, "Введите логин.");
                else if (value.Trim().Length < 3)
                    AddError(propertyName, "Логин должен быть не короче 3 символов.");
                break;

            case nameof(UserPassword):
                if (string.IsNullOrWhiteSpace(value))
                    AddError(propertyName, "Введите пароль.");
                else if (value.Length < 6)
                    AddError(propertyName, "Пароль должен содержать минимум 6 символов.");
                break;
            default:
                break;
        }

        UpdateErrorString(propertyName);
    }

    private void ValidateAll()
    {
        ValidateProperty(nameof(NameSurname), NameSurname);
        ValidateProperty(nameof(ClientPhoneNumber), ClientPhoneNumber);
        ValidateProperty(nameof(ClientEmail), ClientEmail);
        ValidateProperty(nameof(UserLogin), UserLogin);
        ValidateProperty(nameof(UserPassword), UserPassword);
    }

    [ObservableProperty]
    public partial string NameSurname { get; set; }

    partial void OnNameSurnameChanged(string value) => ValidateProperty(nameof(NameSurname), value);

    [ObservableProperty]
    public partial string ClientPhoneNumber { get; set; }

    partial void OnClientPhoneNumberChanged(string value) => ValidateProperty(nameof(ClientPhoneNumber), value);

    [ObservableProperty]
    public partial string ClientEmail { get; set; }

    partial void OnClientEmailChanged(string value) => ValidateProperty(nameof(ClientEmail), value);

    [ObservableProperty]
    public partial string UserLogin { get; set; }

    partial void OnUserLoginChanged(string value) => ValidateProperty(nameof(UserLogin), value);

    [ObservableProperty]
    public partial string UserPassword { get; set; }

    partial void OnUserPasswordChanged(string value) => ValidateProperty(nameof(UserPassword), value);

    [RelayCommand]
    private async Task Register()
    {
        ValidateAll();
        if (HasErrors)
        {
            var allErrors = string.Join(Environment.NewLine, GetErrors(null).Cast<string>());
            await Shell.Current.DisplayAlert("Ошибка валидации", allErrors, "ОК");
            return;
        }

        var words = (NameSurname ?? string.Empty).Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var request = new ClientRegisterRequest
        {
            Name = words.Length > 1 ? words[1] : "Имя",
            Surname = words.Length > 0 ? words[0] : "Фамилия",
            PhoneNumber = ClientPhoneNumber,
            Email = ClientEmail,
            RoleId = (int)RoleEnum.Client,
            Login = UserLogin,
            Password = UserPassword,
        };

        var regResult = await registrationService.RegisterUser(request);

        if (regResult.Success)
        {
            var loginRequest = new LoginRequest
            {
                Login = UserLogin,
                Password = UserPassword,
            };

            var result = await loginService.LoginUser(loginRequest);
            if (result != null)
                await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            var message = string.IsNullOrWhiteSpace(regResult.ErrorMessage)
                ? "Не удалось зарегистрироваться. Попробуйте позже."
                : regResult.ErrorMessage;
            await Shell.Current.DisplayAlert("Ошибка", message, "ОК");
        }
    }

    [RelayCommand]
    private static async Task OnGoToLogin() => await Shell.Current.GoToAsync("//LoginPage");
    [GeneratedRegex(@"^(\+375|80)(29|44|33|25)\d{7}$")]
    private static partial Regex MyRegex { get; }


}

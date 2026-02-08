using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CRMSystemMobile.Services;
using Shared.Contracts.Bill;
using Shared.Contracts.PaymentNote;
using Shared.Enums;
using Shared.Filters;
using System.Collections.ObjectModel;

namespace CRMSystemMobile.ViewModels;

public partial class BillDetailsViewModel : ObservableObject, IQueryAttributable
{
    private readonly PaymentService _paymentService;
    private readonly BillService _billService;

    public BillDetailsViewModel(PaymentService paymentService, BillService billService)
    {
        _paymentService = paymentService;
        _billService = billService;
    }

    [ObservableProperty]
    public partial BillResponse Bill { get; set; }

    [ObservableProperty]
    public partial decimal PaymentAmount { get; set; }

    [ObservableProperty]
    public partial decimal RemainingDebt { get; set; }

    [ObservableProperty]
    public partial DateTime PaymentDate { get; set; } = DateTime.Now;
    public List<string> PaymentMethods { get; } = ["Картой", "Наличными", "ЕРИП"];

    [ObservableProperty]
    public partial string SelectedMethodName { get; set; } = "Картой";

    [ObservableProperty]
    public partial bool IsBusy { get; set; }

    [ObservableProperty]
    public partial bool CanPay { get; set; }
    public ObservableCollection<PaymentNoteResponse> Payments { get; } = [];

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("Bill"))
        {
            Bill = (BillResponse)query["Bill"];
            if (Bill != null)
            {
                RemainingDebt = Bill.Amount;
                UpdateDebtInfoCommand.Execute(null);
                LoadBillPaymentsCommand.Execute(null);
            }
        }
    }

    [RelayCommand]
    private async Task UpdateDebtInfo()
    {
        if (Bill == null) return;

        var debt = await _billService.GetBillDebt(Bill.Id);

        if (debt.HasValue)
        {
            RemainingDebt = debt.Value;

            CanPay = RemainingDebt > 0;

            if (CanPay)
            {
                PaymentAmount = RemainingDebt;
            }
            else
            {
                PaymentAmount = 0;
            }
        }
    }

    [RelayCommand]
    private async Task LoadBillPayments()
    {
        if (Bill == null) return;

        try
        {
            var filter = new PaymentNoteFilter(
                BillIds: [Bill.Id],
                MethodIds: [],
                SortBy: "date",
                Page: 1,
                Limit: 100,
                IsDescending: true
            );

            var (items, _) = await _paymentService.GetMyPayments(filter);

            Payments.Clear();
            if (items != null)
            {
                foreach (var item in items)
                {
                    Payments.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading payments: {ex}");
        }
    }

    [RelayCommand]
    private async Task MakePayment()
    {
        if (IsBusy || Bill == null) return;

        if (PaymentAmount > RemainingDebt)
        {
            bool confirm = await Shell.Current.DisplayAlert("Внимание",
                $"Сумма платежа ({PaymentAmount}) больше текущего долга ({RemainingDebt}). Продолжить?",
                "Да", "Нет");
            if (!confirm) return;
        }

        if (PaymentAmount <= 0)
        {
            await Shell.Current.DisplayAlert("Ошибка", "Сумма должна быть больше нуля", "ОК");
            return;
        }

        IsBusy = true;

        try
        {
            var methodEnum = SelectedMethodName switch
            {
                "Картой" => PaymentMethodEnum.Card,
                "Наличными" => PaymentMethodEnum.Cash,
                "ЕРИП" => PaymentMethodEnum.Erip,
                _ => PaymentMethodEnum.Card
            };

            var request = new PaymentNoteRequest
            {
                BillId = Bill.Id,
                Amount = PaymentAmount,
                Date = PaymentDate,
                MethodId = methodEnum
            };

            var error = await _paymentService.CreatePayment(request);

            if (error == null)
            {
                await Shell.Current.DisplayAlert("Успех", "Оплата проведена!", "ОК");

                await LoadBillPayments();
                await UpdateDebtInfo();
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка", error, "ОК");
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }
}
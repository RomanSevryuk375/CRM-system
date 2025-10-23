namespace CRMSystem.Core.Models;

public class PaymentJournal
{
    public PaymentJournal(int id, int billId, DateTime date, decimal amount, string method)
    {
        Id = id;
        BillId = billId;
        Date = date; 
        Amount = amount;
        Method = method;
    }
    public int Id { get; }

    public int BillId { get; }

    public DateTime Date { get; }

    public decimal Amount { get; }

    public string Method { get; } = string.Empty;

    public static (PaymentJournal paymentJournal, string error) Create (int id, int billId, DateTime date, decimal amount, string method)
    {
        var error = string.Empty;
        var allowedMethods = new[] { "Картой", "Наличными", "ЕРИП", "Рассрочка", "Другое" };

        var paymentJournal = new PaymentJournal(id, billId, date, amount, method);

        if (id <= 0)
            error = "Id must be positive";

        if (billId <= 0)
            error = "BillId must be positive";

        if (date > DateTime.Now)
            error = "Payment date cannot be in the future";

        if (amount <= 0)
            error = "Payment amount must be greater than 0";

        if (string.IsNullOrWhiteSpace(method))
            error = "Payment method cannot be empty";

        if (!allowedMethods.Contains(method))
            error = $"Invalid payment method. Allowed: {string.Join(", ", allowedMethods)}";

        return (paymentJournal, error);
    }
}

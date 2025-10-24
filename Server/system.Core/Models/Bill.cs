namespace CRMSystem.Core.Models;

public class Bill
{
    public Bill(int id, int orderId, int statusId, DateTime date, decimal amount, DateTime? actualBillDate)
    {
        Id = id;
        OrderId = orderId;
        StatusId = statusId;
        Date = date;
        Amount = amount;
        ActualBillDate = actualBillDate;
    }

    public int Id { get; }

    public int OrderId { get; }

    public int StatusId { get; }

    public DateTime Date { get; }

    public decimal Amount { get; }

    public DateTime? ActualBillDate { get; }

    public DateTime LastBillDate => Date.AddDays(14);

    public static (Bill bills, string error) Create (int id, int orderId, int statusId, DateTime date, decimal amount, DateTime? actualBillDate)
    {
        var error = string.Empty;
        var allowedStatuses = new[] { 1, 2, 3 };

        if (orderId <= 0)
            error = "Order ID must be positive";

        if (statusId <= 0)
            error = "Status ID must be positive";

        if (!allowedStatuses.Contains(statusId))
            error = "Invalid bill status";

        if (date > DateTime.Now)
            error = "Bill date cannot be in the future";

        if (amount <= 0)
            error = "Bill amount must be greater than 0";

        if (actualBillDate.HasValue)
        {
            if (actualBillDate.Value > DateTime.Now)
                error = "Actual closing date cannot be in the future";

            if (actualBillDate.Value < date)
                error = "Actual closing date cannot be earlier than bill date";

            if (statusId == 1 && !actualBillDate.HasValue)
                error = "Actual closing date must be set for paid bills";
        }
        else
        {
            if (statusId == 1)
                error = "Actual closing date must be set for paid bills";
        }

        var bills = new Bill(id, orderId, statusId, date, amount, actualBillDate);
        return (bills, error);
    }
}

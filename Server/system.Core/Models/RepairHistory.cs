namespace CRMSystem.Core.Models;

public class RepairHistory
{
    public RepairHistory(int id, int OrderId, int carId, DateTime workDate)
    {
        Id = id;
        this.OrderId = OrderId;
        CarId = carId;
        WorkDate = workDate;
    }
    public int Id { get; }
    public int OrderId { get; }
    public int CarId { get; }
    public DateTime WorkDate { get; }
    public decimal ServiceSum { get; }

    public static (RepairHistory repairHistory, string error) Create(int id, int orderId, int carId, DateTime workDate, decimal serviceSum)
    {
        var error = string.Empty;

        if (workDate >= DateTime.Now)
            error = "History can't be in future";

        if (id < 0)
            error = "Id cannot be negative";

        if (orderId <= 0)
            error = "OrderId must be positive";

        if (carId <= 0)
            error = "CarId must be positive";

        if (workDate >= DateTime.Now)
            error = "History can't be in future";

        if (workDate.Year < 1900)
            error = "Work date is too old";

        if (serviceSum <= 0)
            error = "Service sum can't be negative or 0";

        var repairHistory = new RepairHistory(id, orderId, carId, workDate);

        return (repairHistory, error);
    }

}

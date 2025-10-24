namespace CRMSystem.Core.Models;

public class Order
{
    public Order(int id, int statusId, int carId, DateTime date, string priority)
    {
        Id = id;
        StatusId = statusId;
        CarId = carId;
        Date = date;
        Priority = priority;
    }
    public int Id { get; }

    public int StatusId { get; }

    public int CarId { get; }

    public DateTime Date { get; }

    public string Priority { get; } = string.Empty;

    public static (Order order, string error) Create(int id, int statusId, int carId, DateTime date, string priority)
    {
        var error = string.Empty;
        var allowedProirites = new[] {"Повышенный", "Общий", "Низкий"};

        if (statusId <= 0)
            error = "Status Id must be positive";

        if (carId <= 0)
            error = "Car Id must be positive";

        if (date > DateTime.Now)
            error = "Order date cannot be in the future";

        if (!allowedProirites.Contains(priority))
            error = "Invalid bill priority";

        var order = new Order(id, statusId, carId, date, priority);

        return (order, error);
     }
}

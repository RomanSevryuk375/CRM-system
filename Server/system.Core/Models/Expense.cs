using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRMSystem.Core.Models;

public class Expense
{
    public Expense(int id, DateTime date, string category, int? taxId, int? usedPartId, string expenseType, decimal sum)
    {
        Id = id;
        Date = date;
        Category = category;
        TaxId = taxId;
        UsedPartId = usedPartId;
        ExpenseType = expenseType;
        Sum = sum;
    }
    public int Id { get; }

    public DateTime Date { get; }

    public string Category { get; } 

    public int? TaxId { get; }

    public int? UsedPartId { get; }

    public string ExpenseType { get; } 

    public decimal Sum { get; } 

    public static (Expense expense, string error) Create (int id, DateTime date, string category, int? taxId, int? usedPartId, string expenseType, decimal sum)
    {
        var error = string.Empty;
        var allowedType = new[] {"Переменный", "Постоянный", "Капитальный"};
        var allowedCategory = new[] {
        "Зарплата",
        "Запчасти",
        "Аренда",
        "Коммунальные",
        "Реклама",
        "Оборудование",
        "Хозяйственные",
        "Налоги",
        "Ремонт",
        "Связь",
        "Страхование"};

        if (date > DateTime.Now)
            error = "Date cannot be in the future";

        if (string.IsNullOrWhiteSpace(category))
            error = "Expense category can't be empty";
        if (!allowedCategory.Contains(category))
            error = "Invalid expense category";

        if (string.IsNullOrWhiteSpace(expenseType))
            error = "Expense type can't be empty";
        if (!allowedType.Contains(expenseType))
            error = "Invalid expense type";

        if (sum < 0)
            error = "Sum can't be negative or zero";

        var expense = new Expense(id, date, category, taxId, usedPartId, expenseType, sum);

        return (expense, error);
    }
}

namespace CRMSystem.Core.Models;

public class WorkType
{
    private const int MAX_TITLE_LENGTH = 256;

    public WorkType(int id, string title, string category, string description, decimal standardTime)
    {
        Id = id;
        Title = title;
        Category = category;
        Description = description;
        StandardTime = standardTime;
    }
    public int Id { get; }

    public string Title { get; } = string.Empty;

    public string Category { get; } = string.Empty;

    public string Description { get; } = string.Empty;

    public decimal StandardTime { get; }

    public static (WorkType workType, string error) Create(int id, string title, string category, string description, decimal standardTime)
    {
        var error = string.Empty;

        var allowedCategories = new[] {"Диагностика и обслуживание", "Двигатель и система выпуска", "Ходовая часть и рулевое", "Тормозная система", "Трансмиссия", "Шины и колеса", "Электрика и электроника", "Кузовные работы"};

        if (string.IsNullOrWhiteSpace(title))
            error = "Work type can't be empty";
        if (title.Length > MAX_TITLE_LENGTH)
            error = $"Work type can't be longer";

        if (string.IsNullOrWhiteSpace(category))
            error = "Category can't be empty";
        if (!allowedCategories.Contains(category))
            error = $"Invalid category. Allowed: {string.Join(", ", allowedCategories)}";

        if (string.IsNullOrWhiteSpace(description))
            error = "Description can't be empty";

        if (standardTime <= 0)
            error = "Standard time can't be negative or zero";        

        var workType = new WorkType(id, title, category, description, standardTime);

        return (workType, error);
    }
}

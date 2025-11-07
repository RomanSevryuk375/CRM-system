using System.Net.NetworkInformation;

namespace CRMSystem.Core.Models;

public class UsedPart
{
    private const int MAX_NAME_LENGTH = 256;
    private const int MAX_ARTICLE_LENGTH = 128;

    public UsedPart(int id, int orderId, int supplierId, string name, string article, decimal quantity, decimal unitPrice, decimal? sum)
    {
        Id = id;
        OrderId = orderId;
        SupplierId = supplierId;
        Name = name;
        Article = article;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Sum = sum;
    }
    public int Id { get; }

    public int OrderId { get; }

    public int SupplierId { get; }

    public string Name { get; }

    public string Article { get; }

    public decimal Quantity { get; }

    public decimal UnitPrice { get; }

    public decimal? Sum { get; }

    public static (UsedPart usedPart, string error) Create (int id, int orderId, int supplierId, string name, string article, decimal quantity, decimal unitPrice, decimal? sum)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(name))
            error = "Name can't be empty";
        if (name.Length > MAX_NAME_LENGTH)
            error = $"Name can't be longer than {MAX_NAME_LENGTH} symbols";

        if (string.IsNullOrWhiteSpace(article))
            error = "Article can't be empty";
        if (article.Length > MAX_ARTICLE_LENGTH)
            error = $"Article can't be longer than {MAX_ARTICLE_LENGTH} symbols";

        if (quantity <= 0)
            error = "Quantity can't be negative or zero";

        if (unitPrice <= 0)
            error = "Unit price can't be negative or zero";

        var usedPart = new UsedPart(id, orderId, supplierId, name, article, quantity, unitPrice, sum);

        return (usedPart, error);
    }
}

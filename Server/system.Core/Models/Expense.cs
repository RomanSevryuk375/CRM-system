using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;
using Shared.Enums;


namespace CRMSystem.Core.Models;

public class Expense
{
    private Expense(long id, DateTime date, string category, int? taxId, long? partSetId, ExpenseTypeEnum expenseTypeId, decimal sum)
    {
        Id = id;
        Date = date;
        Category = category;
        TaxId = taxId;
        PartSetId = partSetId;
        ExpenseTypeId = expenseTypeId;
        Sum = sum;
    }
    public long Id { get; }
    public int? TaxId { get; }
    public long? PartSetId { get; }
    public DateTime Date { get; }
    public string Category { get; }
    public ExpenseTypeEnum ExpenseTypeId { get; }
    public decimal Sum { get; }

    public static (Expense? expense, List<string> errors) Create(long id, DateTime date, string category, int? taxId, long? partSetId, ExpenseTypeEnum expenseTypeId, decimal sum)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var typeIdError = DomainValidator.ValidateId(expenseTypeId, "typeId");
        if (!string.IsNullOrEmpty(typeIdError)) errors.Add(typeIdError);

        var dateError = DomainValidator.ValidateDate(date, "date");
        if (!string.IsNullOrEmpty(dateError)) errors.Add(dateError);

        var categoryError = DomainValidator.ValidateString(category, ValidationConstants.MAX_CATEGORY_LENGTH, "category");
        if (!string.IsNullOrEmpty(categoryError)) errors.Add(categoryError);

        var sumError = DomainValidator.ValidateMoney(sum, "sum");
        if (!string.IsNullOrEmpty(sumError)) errors.Add(sumError);

        if (errors.Any())
            return (null, errors);

        var expense = new Expense(id, date, category, taxId, partSetId, expenseTypeId, sum);

        return (expense, new List<string>());
    }
}

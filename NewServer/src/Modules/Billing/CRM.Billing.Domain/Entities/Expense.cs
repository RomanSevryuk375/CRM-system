using CRM.Billing.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Domain.Entities;

public sealed class Expense : AggregateRoot<ExpenseId>, ISoftDeletable, IAuditable
{
    public const int MaxCategoryLength = 128;
    public const int MaxDescriptionLength = 2000;

    private Expense(
        ExpenseId id,
        DateOnly date,
        string category,
        string? description,
        ExpenseType type,
        Money amount,
        TaxId? taxId,
        Guid? referenceId)
    {
        Id = id;
        Date = date;
        Category = category;
        Description = description;
        Type = type;
        Amount = amount;
        TaxId = taxId;
        ReferenceId = referenceId;
    }

#pragma warning disable CS8618
    private Expense() { }
#pragma warning restore CS8618

    public DateOnly Date { get; private set; }
    public string Category { get; private set; }
    public string? Description { get; private set; }
    public ExpenseType Type { get; private set; }
    public Money Amount { get; private set; }
    public TaxId? TaxId { get; private set; }
    public Guid? ReferenceId { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Expense> Create(
        ExpenseId id,
        DateOnly date,
        string category,
        string? description,
        ExpenseType type,
        decimal amount,
        DateOnly today,
        TaxId? taxId = null,
        Guid? referenceId = null)
    {
        List<Error> errors = [];

        if (date > today)
        {
            errors.Add(Error.Validation<Expense>(Errors.FutureDate));
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            errors.Add(Error.Validation<Expense>(Errors.CategoryEmpty));
        }
        else if (category.Length > MaxCategoryLength)
        {
            errors.Add(Error.Validation<Expense>(Errors.CategoryTooLong));
        }

        if (description?.Length > MaxDescriptionLength)
        {
            errors.Add(Error.Validation<Expense>(Errors.DescriptionTooLong));
        }

        Result<Money> amountResult = Money.Create(amount);
        if (amountResult.IsFailure)
        {
            errors.Add(amountResult.Error);
        }
        else if (amountResult.Value.Value == 0)
        {
            errors.Add(Error.Validation<Expense>(Errors.ZeroAmount));
        }

        if (errors.Count != 0)
        {
            return Result<Expense>.Failure(Error.Validation<Expense>(
                string.Join("; ", errors.Select(x => x.Message))));
        }

        Expense expense = new(
            id,
            date,
            category,
            description,
            type,
            amountResult.Value,
            taxId,
            referenceId);

        expense.IncrementVersion();

        return Result<Expense>.Success(expense);
    }

    public Result UpdateDetails(
        DateOnly newDate,
        string category,
        string? description,
        DateOnly today)
    {
        List<Error> errors = [];

        if (newDate > today)
        {
            errors.Add(Error.Validation<Expense>(Errors.FutureDate));
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            errors.Add(Error.Validation<Expense>(Errors.CategoryEmpty));
        }
        else if (category.Length > MaxCategoryLength)
        {
            errors.Add(Error.Validation<Expense>(Errors.CategoryTooLong));
        }

        if (description?.Length > MaxDescriptionLength)
        {
            errors.Add(Error.Validation<Expense>(Errors.DescriptionTooLong));
        }

        if (errors.Count != 0)
        {
            return Result.Failure(Error.Validation<Expense>(
                string.Join("; ", errors.Select(x => x.Message))));
        }

        Date = newDate;
        Category = category;
        Description = description;

        IncrementVersion();

        return Result.Success();
    }

    public Result AssignTax(TaxId taxId)
    {
        TaxId = taxId;

        IncrementVersion();

        return Result.Success();
    }

    public static class Errors
    {
        public const string FutureDate = "Date cannot be in the future.";
        public const string CategoryEmpty = "Category cannot be empty.";
        public static readonly string CategoryTooLong = $"Category should be shorter than {MaxCategoryLength} symbols.";
        public static readonly string DescriptionTooLong = $"Description should be shorter than {MaxDescriptionLength} symbols.";
        public const string ZeroAmount = "Expense amount must be greater than zero.";
    }
}
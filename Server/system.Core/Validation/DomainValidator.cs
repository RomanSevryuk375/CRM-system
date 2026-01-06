// Ignore Spelling: Validator

namespace CRMSystem.Core.Validation;

public static class DomainValidator
{
    public static string ValidateId (int Id, string FieldName)
    {
        if (Id < 0)
            return $"{FieldName} must be positive";

        return string.Empty;
    }
    public static string ValidateId(long Id, string FieldName)
    {
        if (Id < 0)
            return $"{FieldName} must be positive";

        return string.Empty;
    }
    public static string ValidateId(Enum Id, string FieldName)
    {
        if (Convert.ToInt32(Id) <= 0)
            return $"{FieldName} must be positive";

        return string.Empty;
    }

    public static string ValidateString (string? Value, string FieldName)
    {
        if (string.IsNullOrWhiteSpace(Value))
            return $"{FieldName} can't be empty";

        return string.Empty;
    }

    public static string ValidateString(string? Value, int Length, string FieldName)
    {
        if (string.IsNullOrWhiteSpace(Value))
            return $"{FieldName} can't be empty";

        if (Value.Length > Length)
            return $"Name can't be longer than {Length} symbols";

        return string.Empty;
    }

    public static string ValidateDate(DateTime? Date, string FieldName)
    {
        if (!Date.HasValue)
            return $"{FieldName} can't be empty";

        if (Date > DateTime.Now)
            return $"Acceptance date cannot be in the future";

        return string.Empty;
    }
    public static string ValidateDate(DateOnly? Date, string FieldName)
    {
        if (!Date.HasValue)
            return $"{FieldName} can't be empty";

        if (Date > DateOnly.FromDateTime(DateTime.Now))
            return $"{FieldName} can't be in the future";

        return string.Empty;
    }

    public static string ValidateDateRange(DateTime DateStart, DateTime? DateEnd)
    {
        if (DateEnd.HasValue && DateEnd.Value < DateStart)
            return "End date cant be earlier than start date";

        return string.Empty;
    }
    public static string ValidateDateRange(DateOnly DateStart, DateOnly? DateEnd)
    {
        if (DateEnd.HasValue && DateEnd.Value < DateStart)
            return "End date cant be earlier than start date";

        return string.Empty;
    }
    public static string ValidateDateRange(TimeOnly DateStart, TimeOnly? DateEnd)
    {
        if (DateEnd.HasValue && DateEnd.Value < DateStart)
            return "End date cant be earlier than start date";

        return string.Empty;
    }
    public static string ValidateDateRange(DateTime DateStart, DateOnly? DateEnd)
    {
        var Value = DateOnly.FromDateTime(DateStart);
        if (DateEnd.HasValue && DateEnd.Value < Value)
            return "End date cant be earlier than start date";

        return string.Empty;
    }

    public static string ValidateMoney(decimal Value, string FieldName)
    {
        if (Value < 0)
            return $"{FieldName} can't be negative";

        return string.Empty;
    }
}

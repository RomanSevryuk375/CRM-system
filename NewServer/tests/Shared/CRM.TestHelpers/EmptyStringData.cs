using Xunit;

namespace CRM.TestHelpers;

public static class EmptyStringData
{
    public static TheoryData<string?> Values => new()
    {
        null, string.Empty, "   "
    };
}

using System.Runtime.Serialization;

namespace CRMSystem.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string? message) : base(message)
    {
    }
}

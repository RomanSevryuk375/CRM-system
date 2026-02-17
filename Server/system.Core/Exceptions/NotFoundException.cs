using System.Runtime.Serialization;

namespace CRMSystem.Core.Exceptions;

public class NotFoundException(string? message) : Exception(message);

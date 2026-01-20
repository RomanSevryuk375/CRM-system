namespace Shared.Contracts.Attachment;

public record AttachmentResponse
{
    public long Id { get; init; }
    public long OrderId { get; init; }
    public string Worker { get; init; } = string.Empty;
    public int WorkerId { get; init; }
    public DateTime CreateAt { get; init; }
    public string? Description { get; init; }
};

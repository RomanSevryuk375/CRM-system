// Ignore Spelling: Img

namespace Shared.Filters;

public record AttachmentImgFilter
(
    IEnumerable<long>? AttachmentIds,
    int Page,
    int Limit
);

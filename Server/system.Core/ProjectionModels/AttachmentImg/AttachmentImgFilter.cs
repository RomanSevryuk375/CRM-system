// Ignore Spelling: Img

namespace CRMSystem.Core.ProjectionModels.AttachmentImg;

public record AttachmentImgFilter
(
    IEnumerable<long> AttachmentIds,
    int Page,
    int Limit
);

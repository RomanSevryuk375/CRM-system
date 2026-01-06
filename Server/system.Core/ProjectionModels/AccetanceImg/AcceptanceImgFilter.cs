// Ignore Spelling: Img

namespace CRMSystem.Core.ProjectionModels.AccetanceImg;

public record AcceptanceImgFilter
(
    IEnumerable<long> AcceptanceIds,
    int Page,
    int Limit
);

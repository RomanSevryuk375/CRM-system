// Ignore Spelling: Img

namespace Shared.Filters;

public record AcceptanceImgFilter
(
    IEnumerable<long> AcceptanceIds,
    int Page,
    int Limit
);

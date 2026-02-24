using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.Order;

public record OrderCreateModel
(
    OrderStatusEnum StatusId,
    long CarId,
    DateOnly Date,
    string? OrderPdfFileName,
    string? OrderAgreementPdfFileName,
    OrderPriorityEnum PriorityId);
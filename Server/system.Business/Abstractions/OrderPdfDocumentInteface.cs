using CRMSystem.Core.ProjectionModels.Order;

namespace CRMSystem.Business.Abstractions;

public interface IOrderPdfService
{
    byte[] GenerateOrderPdf(OrderDocumentModel data);
}
using CRMSystem.Core.ProjectionModels.Order;

namespace CRMSystem.Business.Abstractions;

public interface IOrderPdfDocument
{
    byte[] GenerateOrderPdf(OrderDocumentModel data);
}
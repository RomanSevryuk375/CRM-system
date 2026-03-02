using AutoMapper;
using CRMSystem.Core.ProjectionModels.OrderStatus;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class OrderStatusProfile : Profile
{
    public OrderStatusProfile()
    {
        CreateMap<OrderStatusItem, OrderStatusResponse>();

        CreateMap<OrderStatusEntity, OrderStatusItem>();
    }
}

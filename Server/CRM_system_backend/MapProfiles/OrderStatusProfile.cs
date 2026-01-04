using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class OrderStatusProfile : Profile
{
    public OrderStatusProfile()
    {
        CreateMap<OrderStatusItem, OrderStatusResponse>();

        CreateMap<OrderStatusEntity, OrderStatusItem>();
    }
}

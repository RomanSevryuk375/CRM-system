using AutoMapper;
using CRM_system_backend.Contracts.Supplier;
using CRMSystem.Core.DTOs.Supplier;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<SupplierItem, SupplierResponse>();

        CreateMap<SupplierEntity, SupplierItem>();
    }
}

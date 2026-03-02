using AutoMapper;
using CRMSystem.Core.ProjectionModels.Supplier;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts.Supplier;

namespace CRM_system_backend.MapProfiles;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<SupplierItem, SupplierResponse>();

        CreateMap<SupplierEntity, SupplierItem>();

        CreateMap<SupplierRequest, SupplierCreateModel>();
        
        CreateMap<SupplierUpdateRequest, SupplierUpdateModel>();
    }
}

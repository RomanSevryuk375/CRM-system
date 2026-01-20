using AutoMapper;
using CRMSystem.Core.ProjectionModels.Shift;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.Shift;

namespace CRM_system_backend.MapProfiles;

public class ShiftProfile : Profile
{
    public ShiftProfile()
    {
        CreateMap<ShiftItem, ShiftResponse>();

        CreateMap<ShiftEntity, ShiftItem>();
    }
}

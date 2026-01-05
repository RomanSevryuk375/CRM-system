using AutoMapper;
using CRM_system_backend.Contracts.Shift;
using CRMSystem.Core.DTOs.Shift;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class ShiftProfile : Profile
{
    public ShiftProfile()
    {
        CreateMap<ShiftItem, ShiftResponse>();

        CreateMap<ShiftEntity, ShiftItem>();
    }
}

using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class WorkInOrderStatusProfile : Profile
{
    public WorkInOrderStatusProfile()
    {
        CreateMap<WorkInOrderStatusItem, WorkInOrderStatusResponse>();

        CreateMap<WorkInOrderStatusEntity, WorkInOrderStatusItem>();
    }
}

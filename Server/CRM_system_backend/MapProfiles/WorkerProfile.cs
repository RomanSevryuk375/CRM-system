using AutoMapper;
using CRM_system_backend.Contracts.Worker;
using CRMSystem.Core.DTOs.Worker;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class WorkerProfile : Profile
{
    public WorkerProfile()
    {
        CreateMap<WorkerItem, WorkerResponse>();

        CreateMap<WorkerEntity, WorkerItem>();
    }
}

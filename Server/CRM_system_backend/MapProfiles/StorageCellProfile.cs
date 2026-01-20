using AutoMapper;
using CRMSystem.Core.ProjectionModels.StorageCell;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.StorageCell;

namespace CRM_system_backend.MapProfiles;

public class StorageCellProfile : Profile
{
    public StorageCellProfile()
    {
        CreateMap<StorageCellItem, StorageCellResponse>();

        CreateMap<StorageCellEntity, StorageCellItem>();
    }
}

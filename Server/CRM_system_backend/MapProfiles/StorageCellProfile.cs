using AutoMapper;
using CRM_system_backend.Contracts.StorageCell;
using CRMSystem.Core.ProjectionModels.StorageCell;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class StorageCellProfile : Profile
{
    public StorageCellProfile()
    {
        CreateMap<StorageCellItem, StorageCellResponse>();

        CreateMap<StorageCellEntity, StorageCellItem>();
    }
}

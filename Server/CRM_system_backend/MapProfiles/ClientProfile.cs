using AutoMapper;
using CRM_system_backend.Contracts.Client;
using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientItem, ClientsResponse>();

        CreateMap<ClientEntity, ClientItem>();
    }
}

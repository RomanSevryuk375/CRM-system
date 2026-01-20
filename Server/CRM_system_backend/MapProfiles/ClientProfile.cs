using AutoMapper;
using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.Client;

namespace CRM_system_backend.MapProfiles;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientItem, ClientsResponse>();

        CreateMap<ClientEntity, ClientItem>();
    }
}

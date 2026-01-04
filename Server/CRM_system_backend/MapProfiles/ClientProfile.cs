using AutoMapper;
using CRMSystem.Core.DTOs.Client;
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

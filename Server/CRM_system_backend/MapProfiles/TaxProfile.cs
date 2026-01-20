using AutoMapper;
using CRMSystem.Core.ProjectionModels.Tax;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.Tax;

namespace CRM_system_backend.MapProfiles;

public class TaxProfile : Profile
{
    public TaxProfile()
    {
        CreateMap<TaxItem, TaxResponse>();

        CreateMap<TaxEntity, TaxItem>()
            .ForMember(dest => dest.Type,
                        opt => opt.MapFrom(src => $"{src.TaxType!.Name}"));
    }
}

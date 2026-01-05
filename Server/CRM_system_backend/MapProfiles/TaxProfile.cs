using AutoMapper;
using CRM_system_backend.Contracts.Tax;
using CRMSystem.Core.DTOs.Tax;
using CRMSystem.DataAccess.Entites;

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

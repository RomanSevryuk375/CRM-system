using AutoMapper;
using CRM_system_backend.Contracts.Bill;
using CRMSystem.Core.ProjectionModels.Bill;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class BillProfile : Profile
{
    public BillProfile()
    {
        CreateMap<BillItem, BillResponse>();

        CreateMap<BillUpdateRequest, BillUpdateModel>();

        CreateMap<BillEntity, BillItem>()
            .ForMember(dest => dest.Status,
                        opt => opt.MapFrom(src => $"{src.Status!.Name}"));
    }
}

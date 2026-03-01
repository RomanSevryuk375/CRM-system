using AutoMapper;
using CRMSystem.Core.ProjectionModels.Expense;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts.Expense;

namespace CRM_system_backend.MapProfiles;

public class ExpenseProfile : Profile
{
    public ExpenseProfile()
    {
        CreateMap<ExpenseItem, ExpenseResponse>();

        CreateMap<ExpenseEntity, ExpenseItem>()
            .ForMember(dest => dest.Tax,
                        opt => opt.MapFrom(src => $"{src.Tax!}"))
            .ForMember(dest => dest.ExpenseType,
                        opt => opt.MapFrom(src => $"{src.ExpenseType!.Name}"));

        CreateMap<ExpenseRequest, ExpenseCreateModel>();
        
        CreateMap<ExpenseUpdateRequest, ExpenseUpdateModel>();
    }
}

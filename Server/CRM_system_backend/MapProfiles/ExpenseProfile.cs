using AutoMapper;
using CRM_system_backend.Contracts.Expense;
using CRMSystem.Core.ProjectionModels.Expense;
using CRMSystem.DataAccess.Entites;

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
    }
}

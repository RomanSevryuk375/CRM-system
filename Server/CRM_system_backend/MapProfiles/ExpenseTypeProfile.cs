using AutoMapper;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class ExpenseTypeProfile : Profile
{
    public ExpenseTypeProfile()
    {
        CreateMap<ExpenseTypeItem, ExpenseTypeResponse>();

        CreateMap<ExpenseTypeEntity, ExpenseTypeItem>();
    }
}

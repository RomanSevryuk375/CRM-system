using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class ExpenseTypeProfile : Profile
{
    public ExpenseTypeProfile()
    {
        CreateMap<ExpenseTypeItem, ExpenseTypeResponse>();

        CreateMap<ExpenseTypeEntity, ExpenseTypeItem>();
    }
}

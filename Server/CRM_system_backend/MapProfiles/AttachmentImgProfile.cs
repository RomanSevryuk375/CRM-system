// Ignore Spelling: Img

using AutoMapper;
using CRM_system_backend.Contracts.AttachmentImg;
using CRMSystem.Core.ProjectionModels.AttachmentImg;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class AttachmentImgProfile : Profile
{
    public AttachmentImgProfile()
    {
        CreateMap<AttachmentImgItem, AttachmentImgResponse>();

        CreateMap<AttachmentImgEntity, AttachmentImgItem>();
    }
}

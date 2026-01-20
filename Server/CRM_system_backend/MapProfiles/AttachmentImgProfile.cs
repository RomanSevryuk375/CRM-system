// Ignore Spelling: Img

using AutoMapper;
using CRMSystem.Core.ProjectionModels.AttachmentImg;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.AttachmentImg;

namespace CRM_system_backend.MapProfiles;

public class AttachmentImgProfile : Profile
{
    public AttachmentImgProfile()
    {
        CreateMap<AttachmentImgItem, AttachmentImgResponse>();

        CreateMap<AttachmentImgEntity, AttachmentImgItem>();
    }
}

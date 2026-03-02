// Ignore Spelling: Img

using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Core.ProjectionModels.AttachmentImg;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts.AttachmentImg;

namespace CRM_system_backend.MapProfiles;

public class AttachmentImgProfile : Profile
{
    public AttachmentImgProfile()
    {
        CreateMap<AttachmentImgItem, AttachmentImgResponse>();

        CreateMap<AttachmentImgEntity, AttachmentImgItem>();

        CreateMap<AttachmentImgRequest, AttachmentImgCreateModel>();
    }
}

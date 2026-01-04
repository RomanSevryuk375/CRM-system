using AutoMapper;
using CRMSystem.Core.DTOs.Attachment;
using CRMSystem.Core.DTOs.AttachmentImg;
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

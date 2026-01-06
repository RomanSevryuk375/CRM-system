using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]


public class NotificationTypeController : ControllerBase
{
    private readonly INotificationTypeService _notificationTypeService;
    private readonly IMapper _mapper;

    public NotificationTypeController(
        INotificationTypeService notificationTypeService,
        IMapper mapper)
    {
        _notificationTypeService = notificationTypeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificationTypeItem>>> GetNotificationTypes()
    {
        var dto = await _notificationTypeService.GetNotificationTypes();

        var response = _mapper.Map<NotificationTypeResponse>(dto);

        return Ok(response);
    }
}

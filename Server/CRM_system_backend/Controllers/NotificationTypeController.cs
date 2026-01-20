using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

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
    public async Task<ActionResult<List<NotificationTypeItem>>> GetNotificationTypes(CancellationToken ct)
    {
        var dto = await _notificationTypeService.GetNotificationTypes(ct);

        var response = _mapper.Map<NotificationTypeResponse>(dto);

        return Ok(response);
    }
}

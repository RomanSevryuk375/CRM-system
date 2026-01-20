using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationStatusController : ControllerBase
{
    private readonly INotificationStatusService _notificationStatusService;
    private readonly IMapper _mapper;

    public NotificationStatusController(
        INotificationStatusService notificationStatusService,
        IMapper mapper)
    {
        _notificationStatusService = notificationStatusService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificationStatusItem>>> GetNotificationStatuses(CancellationToken ct)
    {
        var dto = await _notificationStatusService.GetNotificationStatuses(ct);

        var response = _mapper.Map<NotificationStatusResponse>(dto);

        return Ok(response);
    }
}

using AutoMapper;
using CRMSystem.Business.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/notification-statuses")]
public class NotificationStatusController(
    INotificationStatusService notificationStatusService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<NotificationStatusResponse>>> GetNotificationStatuses(CancellationToken ct)
    {
        var dto = await notificationStatusService.GetNotificationStatuses(ct);
        var response = mapper.Map<NotificationStatusResponse>(dto);

        return Ok(response);
    }
}

using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/v1/notification-statuses")]
[ApiController]
public class NotificationStatusController(
    INotificationStatusService notificationStatusService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<NotificationStatusItem>>> GetNotificationStatuses(CancellationToken ct)
    {
        var dto = await notificationStatusService.GetNotificationStatuses(ct);

        var response = mapper.Map<NotificationStatusResponse>(dto);

        return Ok(response);
    }
}

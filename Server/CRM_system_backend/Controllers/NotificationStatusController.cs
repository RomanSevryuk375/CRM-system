using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationStatusController : ControllerBase
{
    private readonly INotificationStatusService _notificationStatusService;

    public NotificationStatusController(INotificationStatusService notificationStatusService)
    {
        _notificationStatusService = notificationStatusService;
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificationStatusItem>>> GetNotificationStatuses()
    {
        var dto = await _notificationStatusService.GetNotificationStatuses();

        var response = dto.Select(n => new NotificationStatusResponse(
            n.Id,
            n.Name));

        return Ok(response);
    }
}

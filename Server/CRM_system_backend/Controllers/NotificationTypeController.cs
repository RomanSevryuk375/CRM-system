using AutoMapper;
using CRMSystem.Business.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/notification-types")]
public class NotificationTypeController(
    INotificationTypeService notificationTypeService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<NotificationTypeResponse>>> GetNotificationTypes(CancellationToken ct)
    {
        var dto = await notificationTypeService.GetNotificationTypes(ct);

        var response = mapper.Map<NotificationTypeResponse>(dto);

        return Ok(response);
    }
}

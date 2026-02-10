using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace CRM_system_backend.Controllers;

[Route("api/v1/notification-types")]
[ApiController]


public class NotificationTypeController(
    INotificationTypeService notificationTypeService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<NotificationTypeItem>>> GetNotificationTypes(CancellationToken ct)
    {
        var dto = await notificationTypeService.GetNotificationTypes(ct);

        var response = mapper.Map<NotificationTypeResponse>(dto);

        return Ok(response);
    }
}

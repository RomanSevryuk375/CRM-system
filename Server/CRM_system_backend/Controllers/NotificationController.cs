using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Notification;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/notifications")]
public class NotificationController(
    INotificationService notificationService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<NotificationResponse>>> GetPagedNotifications(
        [FromQuery]NotificationFilter filter, CancellationToken ct)
    {
        var dto = await notificationService.GetPagedNotifications(filter, ct);
        var count = await notificationService.GetCountNotifications(filter, ct);

        var response = mapper.Map<List<NotificationResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:long}")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<NotificationResponse>> GetNotificationById(
        long id, CancellationToken ct)
    {
        var dto = await notificationService.GetNotificationById(id, ct);
        var response = mapper.Map<NotificationResponse>(dto);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateNotification(
        NotificationRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<NotificationCreateModel>(request);
        var id = await notificationService.CreateNotification(createModel, ct);
        
        var createdModel = notificationService.GetNotificationById(id, ct);
        var response = mapper.Map<NotificationResponse>(createdModel);

        return CreatedAtAction(
            nameof(GetNotificationById),
            new { id = createdModel.Id },
            response);
    }

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteNotification(
        long id, CancellationToken ct)
    {
        await notificationService.DeleteNotification(id, ct);

        return NoContent();
    }
}

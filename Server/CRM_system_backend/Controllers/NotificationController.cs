using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Notification;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/v1/notifications")]
[ApiController]
public class NotificationController(
    INotificationService notificationService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<NotificationItem>>> GetPagedNotifications(
        [FromQuery]NotificationFilter filter, CancellationToken ct)
    {
        var dto = await notificationService.GetPagedNotifications(filter, ct);
        var count = await notificationService.GetCountNotifications(filter, ct);

        var response = mapper.Map<List<NotificationResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> CreateNotification(
        NotificationRequest request, CancellationToken ct)
    {
        var (notification, errors) = Notification.Create(
            0,
            request.ClientId,
            request.CarId,
            request.TypeId,
            request.StatusId,
            request.Message,
            request.SendAt);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        await notificationService.CreateNotification(notification!, ct);

        return Created();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteNotification(
        long id, CancellationToken ct)
    {
        await notificationService.DeleteNotification(id, ct);

        return NoContent();
    }
}

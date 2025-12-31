using CRM_system_backend.Contracts.Notification;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Notification;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificationItem>>> GetPagedNotifications([FromQuery]NotificationFilter filter)
    {
        var dto = await _notificationService.GetPagedNotifications(filter);
        var count = await _notificationService.GetCountNotifications(filter);

        var response = dto.Select(n => new NotificationResponse(
            n.id,
            n.client,
            n.clientId,
            n.car,
            n.carId,
            n.type,
            n.typeId,
            n.status,
            n.statusId,
            n.message,
            n.sendAt));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateNotification(NotificationRequest request)
    {
        var (notification, errors) = Notification.Create(
            0,
            request.clientId,
            request.carId,
            request.typeId,
            request.statusId,
            request.message,
            request.sendAt);

        if(errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _notificationService.CreateNotification(notification!);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteNotification(long id)
    {
        var Id = await _notificationService.DeleteNotification(id);

        return Ok(Id);
    }
}

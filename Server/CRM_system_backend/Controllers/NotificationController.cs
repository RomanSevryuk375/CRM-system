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
            n.Id,
            n.Client,
            n.ClientId,
            n.Car,
            n.CarId,
            n.Type,
            n.TypeId,
            n.Status,
            n.StatusId,
            n.Message,
            n.SendAt));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateNotification(NotificationRequest request)
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

using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Notification;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public NotificationController(
        INotificationService notificationService,
        IMapper mapper)
    {
        _notificationService = notificationService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<List<NotificationItem>>> GetPagedNotifications([FromQuery]NotificationFilter filter, CancellationToken ct)
    {
        var dto = await _notificationService.GetPagedNotifications(filter, ct);
        var count = await _notificationService.GetCountNotifications(filter, ct);

        var response = _mapper.Map<List<NotificationResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> CreateNotification(NotificationRequest request, CancellationToken ct)
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

        var Id = await _notificationService.CreateNotification(notification!, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<long>> DeleteNotification(long id, CancellationToken ct)
    {
        var Id = await _notificationService.DeleteNotification(id, ct);

        return Ok(Id);
    }
}

using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[Route("api/[controller]")]
[ApiController]


public class NotificationTypeController : ControllerBase
{
    private readonly INotificationTypeService _notificationTypeService;

    public NotificationTypeController(INotificationTypeService notificationTypeService)
    {
        _notificationTypeService = notificationTypeService;
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificationTypeItem>>> GetNotificationTypes()
    {
        var dto = await _notificationTypeService.GetNotificationTypes();

        var response = dto.Select(n => new NotificationTypeResponse(
            n.Id,
            n.Name));

        return Ok(response);
    }
}

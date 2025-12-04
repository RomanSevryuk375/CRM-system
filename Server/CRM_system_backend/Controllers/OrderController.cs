using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<Order>>> GetOrders(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var orders = await _orderService.GetPagedOrders(page, limit);
        var totalCount = await _orderService.GetCountOrders();

        var response = orders
            .Select(o => new OrderResponse(
                o.Id,
                o.StatusId,
                o.CarId,
                o.Date,
                o.Priority))
            .ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("with-info")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<List<OrderWithInfoDto>>> GetOrderWithInfo(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var dtos = await _orderService.GetOrderWithInfo(page, limit);
        var totalCount = await _orderService.GetCountOrders();

        var response = dtos.Select(o => new OrderWithInfoDto(
            o.Id,
            o.StatusName,
            o.CarInfo,
            o.Date,
            o.Priority)).ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);            
    }

    [HttpGet("My")]
    [Authorize(Policy = "UserPolicy")]
    public async Task<ActionResult<List<OrderWithInfoDto>>> GetUserOrders(
        [FromQuery(Name = "_page")] int page,
        [FromQuery(Name = "_limit")] int limit)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var totalCount = await _orderService.GetCountUserOrders(userId);
        var dtos = await _orderService.GetPagedUserOrders(userId, page, limit);

        var response = dtos.Select(o => new OrderWithInfoDto(
            o.Id,
            o.StatusName,
            o.CarInfo,
            o.Date,
            o.Priority)).ToList();

        Response.Headers.Append("x-total-count", totalCount.ToString());

        return Ok(response);
    }

    [HttpGet("InWork")]
    [Authorize(Policy = "WorkerPolicy")]
    public async Task<ActionResult<List<OrderWithInfoDto>>> GetWorkerOrders()
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var dtos = await _orderService.GetWorkerOrders(userId);

        var response = dtos.Select(o => new OrderWithInfoDto(
            o.Id,
            o.StatusName,
            o.CarInfo,
            o.Date,
            o.Priority)).ToList();

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult<int>> CreateOrder([FromBody] OrderRequest request)
    {
        var (order, error) = Order.Create(
            0,
            request.StatusId ?? 0,
            request.CarId ?? 0,
            request.Date ?? DateTime.Now,
            request.Priority ?? "");

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var orderId = await _orderService.CreateOrder(order);

        return Ok(orderId);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> UpdateOrder([FromBody] OrderRequest request, int id)
    {
        var result = await _orderService.UpdateOrder(
            id,
            request.StatusId,
            request.CarId,
            request.Date,
            request.Priority);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult<int>> DeleteOrder(int id)
    {
        var result = await _orderService.DeleteOrder(id);

        return Ok(result);
    }
}

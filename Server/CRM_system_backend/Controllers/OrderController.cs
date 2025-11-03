using CRM_system_backend.Contracts;
using CRMSystem.Buisnes.DTOs;
using CRMSystem.Buisnes.Services;
using CRMSystem.Core.Models;
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

    public async Task<ActionResult<List<Order>>> GetOrders()
    {
        var orders = await _orderService.GetOrders();

        var response = orders
            .Select(o => new OrderResponse(
                o.Id,
                o.StatusId,
                o.CarId,
                o.Date,
                o.Priority))
            .ToList();

        return Ok(response);
    }

    [HttpGet("with-info")]

    public async Task<ActionResult<List<OrderWithInfoDto>>> GetOrderWithInfo()
    {
        var dtos = await _orderService.GetOrderWithInfo();

        var response = dtos.Select(o => new OrderWithInfoDto(
            o.Id,
            o.StatusName,
            o.CarInfo,
            o.Date,
            o.Priority)).ToList();

        return Ok(response);            
    }

    [HttpPost]

    public async Task<ActionResult<int>> CreateOrder([FromBody] OrderRequest request)
    {
        var (order, error) = Order.Create(
            0,
            request.StatusId,
            request.CarId,
            request.Date,
            request.Priority);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(new { error });
        }

        var orderId = await _orderService.CreateOrder(order);

        return Ok(orderId);
    }

    [HttpPut]

    public async Task<ActionResult<int>> UpdateOrder([FromBody] OrderRequest request, int id)
    {
        var result = await _orderService.UpdateOrder(id, request.StatusId, request.CarId, request.Date, request.Priority);

        return Ok(result);
    }

    [HttpDelete]

    public async Task<ActionResult<int>> DeleteOrder(int id)
    {
        var result = await _orderService.DeleteOrder(id);

        return Ok(result);
    }
}

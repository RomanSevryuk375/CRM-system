using CRM_system_backend.Contracts.Order;
using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Order;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderItem>>> GetOrders([FromQuery] OrderFilter filter)
    {
        var dto = await _orderService.GetPagedOrders(filter);
        var count = await _orderService.GetCountOrders(filter);

        var response = dto
            .Select(o => new OrderResponse(
                o.id,
                o.status,
                o.statusId,
                o.car,
                o.carId,
                o.date,
                o.priority,
                o.priorityId));

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateOrder([FromBody] OrderRequest request)
    {
        var (order, errors) = Order.Create(
            0,
            request.statusId,
            request.carId,
            request.date,
            request.priorityId);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _orderService.CreateOrder(order!);

        return Ok(Id);
    }

    [HttpPost("/with-bill")]
    public async Task<ActionResult<long>> CreateOrderWithBill([FromBody] OrderWithBillRequest request)
    {
        var (order, errorsOrder) = Order.Create(
            0,
            request.orderStatusId,
            request.carId,
            request.date,
            request.priorityId);

        if (errorsOrder is not null && errorsOrder.Any())
            return BadRequest(errorsOrder);

        var (bill, errorsBill) = Bill.Create(
            0,
            0,
            request.billStatusId,
            request.createdAt,
            request.amount,
            request.actualBillDate);

        if (errorsBill is not null && errorsBill.Any())
            return BadRequest(errorsBill);

        var Id = await _orderService.CreateOrderWithBill(order!, bill!);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateOrder([FromBody] OrderUpdateReuqest reuqest, int id)
    {
        var Id = await _orderService.UpdateOrder(id, reuqest.priorityId);

        return Ok(Id);
    }

    [HttpPatch("close/{id}")]
    public async Task<ActionResult<long>> CloseOrder(long id)
    {
        var Id = await _orderService.CloseOrder(id);

        return Ok(Id);
    }

    [HttpPatch("complite/{id}")]
    public async Task<ActionResult<long>> CompliteOrder(long id)
    {
        var Id = await _orderService.CompliteOrder(id);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteOrder(long id)
    {
        var result = await _orderService.DeleteOrder(id);

        return Ok(result);
    }
}

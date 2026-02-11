using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Order;
using Shared.Enums;
using Shared.Filters;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/v1/orders")]
public class OrderController(
    IOrderService orderService,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<List<OrderItem>>> GetOrders(
        [FromQuery] OrderFilter filter, CancellationToken ct)
    {
        var dto = await orderService.GetPagedOrders(filter, ct);
        var count = await orderService.GetCountOrders(filter, ct);

        var response = mapper.Map<List<OrderResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> CreateOrder(
        [FromBody] OrderRequest request, CancellationToken ct)
    {
        var (order, errors) = Order.Create(
            0,
            request.StatusId,
            request.CarId,
            request.Date,
            request.PriorityId);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        await orderService.CreateOrder(order!, ct);

        return Created();
    }

    [HttpPost("/bill")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> CreateOrderWithBill(
        [FromBody] OrderWithBillRequest request, CancellationToken ct)
    {
        var (order, errorsOrder) = Order.Create(
            0,
            request.OrderStatusId,
            request.CarId,
            request.Date,
            request.PriorityId);

        if (errorsOrder is not null && errorsOrder.Any())
        {
            return BadRequest(errorsOrder);
        }

        var (bill, errorsBill) = Bill.Create(
            0,
            0,
            request.BillStatusId,
            request.CreatedAt,
            request.Amount,
            request.ActualBillDate);

        if (errorsBill is not null && errorsBill.Any())
        {
            return BadRequest(errorsBill);
        }

        await orderService.CreateOrderWithBill(order!, bill!, ct);

        return Created();
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateOrder(
        [FromBody] OrderUpdateRequest request, int id, CancellationToken ct)
    {
        await orderService.UpdateOrder(id, request.PriorityId, ct);

        return NoContent();
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PathcStatusOrder(
        long id, [FromBody] OrderPatchRequest request, CancellationToken ct)
    {
        if (request.OrderStatus == OrderStatusEnum.Closed)
        {
            await orderService.CloseOrder(id, ct);
        }
        else if (request.OrderStatus == OrderStatusEnum.Completed)
        {
            await orderService.CompleteOrder(id, ct);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteOrder(
        long id, CancellationToken ct)
    {
        await orderService.DeleteOrder(id, ct);

        return NoContent();
    }
}

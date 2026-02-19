using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.ProjectionModels.Bill;
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
        var createModel = mapper.Map<OrderCreateModel>(request);

        await orderService.CreateOrder(createModel, ct);

        return Created();
    }

    [HttpPost("/bill")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> CreateOrderWithBill(
        [FromBody] OrderWithBillRequest request, CancellationToken ct)
    {
        var orderCreateModel = mapper.Map<OrderCreateModel>(request);
        var billCreateModel = mapper.Map<BillCreateModel>(request);

        await orderService.CreateOrderWithBill(orderCreateModel, billCreateModel, ct);

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
        switch (request.OrderStatus)
        {
            case OrderStatusEnum.Closed:
                await orderService.CloseOrder(id, ct);
                break;
            case OrderStatusEnum.Completed:
                await orderService.CompleteOrder(id, ct);
                break;
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

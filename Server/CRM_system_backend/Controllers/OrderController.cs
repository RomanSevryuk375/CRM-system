using AutoMapper;
using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Order;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts.Order;

namespace CRM_system_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(
        IOrderService orderService,
        IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderItem>>> GetOrders([FromQuery] OrderFilter filter, CancellationToken ct)
    {
        var dto = await _orderService.GetPagedOrders(filter, ct);
        var count = await _orderService.GetCountOrders(filter, ct);

        var response = _mapper.Map<List<OrderResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateOrder([FromBody] OrderRequest request, CancellationToken ct)
    {
        var (order, errors) = Order.Create(
            0,
            request.StatusId,
            request.CarId,
            request.Date,
            request.PriorityId);

        if (errors is not null && errors.Any())
            return BadRequest(errors);

        var Id = await _orderService.CreateOrder(order!, ct);

        return Ok(Id);
    }

    [HttpPost("/with-bill")]
    public async Task<ActionResult<long>> CreateOrderWithBill([FromBody] OrderWithBillRequest request, CancellationToken ct)
    {
        var (order, errorsOrder) = Order.Create(
            0,
            request.OrderStatusId,
            request.CarId,
            request.Date,
            request.PriorityId);

        if (errorsOrder is not null && errorsOrder.Any())
            return BadRequest(errorsOrder);

        var (bill, errorsBill) = Bill.Create(
            0,
            0,
            request.BillStatusId,
            request.CreatedAt,
            request.Amount,
            request.ActualBillDate);

        if (errorsBill is not null && errorsBill.Any())
            return BadRequest(errorsBill);

        var Id = await _orderService.CreateOrderWithBill(order!, bill!, ct);

        return Ok(Id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateOrder([FromBody] OrderUpdateRequest request, int id, CancellationToken ct)
    {
        var Id = await _orderService.UpdateOrder(id, request.PriorityId, ct);

        return Ok(Id);
    }

    [HttpPatch("close/{id}")]
    public async Task<ActionResult<long>> CloseOrder(long id, CancellationToken ct)
    {
        var Id = await _orderService.CloseOrder(id, ct);

        return Ok(Id);
    }

    [HttpPatch("complete/{id}")]
    public async Task<ActionResult<long>> CompleteOrder(long id, CancellationToken ct)
    {
        var Id = await _orderService.CompleteOrder(id, ct);

        return Ok(Id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<long>> DeleteOrder(long id, CancellationToken ct)
    {
        var result = await _orderService.DeleteOrder(id, ct);

        return Ok(result);
    }
}

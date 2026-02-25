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
    public async Task<ActionResult<List<OrderResponse>>> GetPagedOrders(
        [FromQuery] OrderFilter filter, CancellationToken ct)
    {
        var dto = await orderService.GetPagedOrders(filter, ct);
        var count = await orderService.GetCountOrders(filter, ct);

        var response = mapper.Map<List<OrderResponse>>(dto);

        Response.Headers.Append("x-total-count", count.ToString());

        return Ok(response);
    }
    
    [HttpGet("{id:long}")]
    [Authorize(Policy = "UniPolicy")]
    public async Task<ActionResult<OrderResponse>> GetOrderById(
        long id, CancellationToken ct)
    {
        var dto = await orderService.GetOrderById(id, ct);
        var response = mapper.Map<OrderResponse>(dto);

        return Ok(response);
    }

    [HttpGet("{id:long}/pdf")]
    [Authorize(Policy = "AdminClientPolicy")]
    public async Task<IActionResult> DownloadOrderPdf(long id, CancellationToken ct)
    {
        var (stream, contentType) = await orderService.GetOrderPdfStream(id, ct);
        
        return File(stream, contentType, $"order_{id}.pdf");
    }

    [HttpPost]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> CreateOrder(
        [FromBody] OrderRequest request, CancellationToken ct)
    {
        var createModel = mapper.Map<OrderCreateModel>(request);
        var id = await orderService.CreateOrder(createModel, ct);
        
        var createdDto = await orderService.GetOrderById(id, ct);
        var response = mapper.Map<OrderResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetOrderById),
            new { id = createdDto.Id },
            response);
    }

    [HttpPost("/bills")]
    [Authorize(Policy = "AdminUserPolicy")]
    public async Task<ActionResult> CreateOrderWithBill(
        [FromBody] OrderWithBillRequest request, CancellationToken ct)
    {
        var orderCreateModel = mapper.Map<OrderCreateModel>(request);
        var billCreateModel = mapper.Map<BillCreateModel>(request);
        var id = await orderService.CreateOrderWithBill(orderCreateModel, billCreateModel, ct);
        
        var createdDto = await orderService.GetOrderById(id, ct);
        var response = mapper.Map<OrderResponse>(createdDto);

        return CreatedAtAction(
            nameof(GetOrderById),
            new { id = createdDto.Id },
            response);
    }
    
    [HttpPost("{id:long}/pdf")]
    public async Task<IActionResult> GeneratePdf(long id, CancellationToken ct)
    {
        var filePath = await orderService.CreateOrderPdfAndUpload(id, ct);
        
        return Ok(new { Message = "PDF generated and uploaded", Path = filePath });
    }

    [HttpPut("{id:int}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> UpdateOrder(
        [FromBody] OrderUpdateRequest request, int id, CancellationToken ct)
    {
        await orderService.UpdateOrder(id, request.PriorityId, ct);

        return NoContent();
    }

    [HttpPatch("{id:long}")]
    public async Task<IActionResult> PatchStatusOrder(
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

    [HttpDelete("{id:long}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<ActionResult> DeleteOrder(
        long id, CancellationToken ct)
    {
        await orderService.DeleteOrder(id, ct);

        return NoContent();
    }
}

using CRM.Billing.Api.DTOs;
using CRM.Billing.Application.Features.PriceLists.Commands.CreatePriceList;
using CRM.Billing.Application.Features.PriceLists.Commands.DeactivatePriceList;
using CRM.Billing.Application.Features.PriceLists.Commands.MakePriceListDefault;
using CRM.Billing.Application.Features.PriceLists.Commands.SetFixedPriceForJob;
using CRM.Billing.Application.Features.PriceLists.Queries.GetActivePriceList;
using CRM.Billing.Application.Features.PriceLists.Queries.GetPriceListById;
using CRM.Billing.Application.Features.PriceLists.Queries.GetPriceLists;
using CRM.Shared.Abstractions.Results;
using CRM.Shared.Infrastructure.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Billing.Api.Controllers;

[Route("api/v1/price-lists")]
public sealed class PriceListsController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetList(
        [FromQuery] int limit = 50,
        [FromQuery] int offset = 0,
        CancellationToken cancellationToken = default)
    {
        GetPriceListsQuery query = new(limit, offset);
        IReadOnlyList<PriceListSummaryDto> response = await Sender.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActive(
        [FromQuery] DateOnly today,
        CancellationToken cancellationToken)
    {
        GetActivePriceListQuery query = new(today);
        ActivePriceListDto? response = await Sender.Send(query, cancellationToken);

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        GetPriceListByIdQuery query = new(id);
        PriceListDetailsDto? response = await Sender.Send(query, cancellationToken);

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePriceListCommand command,
        CancellationToken cancellationToken)
    {
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpPost("{id:guid}/items")]
    public async Task<IActionResult> SetFixedPrice(
        Guid id,
        [FromBody] SetFixedPriceRequest request,
        CancellationToken cancellationToken)
    {
        SetFixedPriceForJobCommand command = new(id, request.JobId, request.FixedPrice);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpPatch("{id:guid}/default")]
    public async Task<IActionResult> MakeDefault(
        Guid id,
        CancellationToken cancellationToken)
    {
        MakePriceListDefaultCommand command = new(id);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(
        Guid id,
        [FromBody] DeactivatePriceListRequest request,
        CancellationToken cancellationToken)
    {
        DeactivatePriceListCommand command = new(id, request.ValidTo);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }
}

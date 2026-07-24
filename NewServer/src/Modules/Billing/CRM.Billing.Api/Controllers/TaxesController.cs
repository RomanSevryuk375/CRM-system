using CRM.Billing.Api.DTOs;
using CRM.Billing.Application.Features.Taxes.Commands.CreateTax;
using CRM.Billing.Application.Features.Taxes.Commands.DeleteTax;
using CRM.Billing.Application.Features.Taxes.Commands.RenameTax;
using CRM.Billing.Application.Features.Taxes.Commands.UpdateTaxRate;
using CRM.Billing.Application.Features.Taxes.Queries.GetTaxById;
using CRM.Billing.Application.Features.Taxes.Queries.GetTaxesList;
using CRM.Shared.Abstractions.Results;
using CRM.Shared.Infrastructure.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Billing.Api.Controllers;

[Route("api/v1/taxes")]
public sealed class TaxesController(ISender sender) : ApiController(sender)
{
    [HttpGet]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken)
    {
        GetTaxesListQuery query = new();
        IReadOnlyList<TaxListItemDto> response = await Sender.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        GetTaxByIdQuery query = new(id);
        TaxDetailsDto? response = await Sender.Send(query, cancellationToken);

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTaxCommand command,
        CancellationToken cancellationToken)
    {
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpPatch("{id:guid}/rate")]
    public async Task<IActionResult> UpdateRate(
        Guid id,
        [FromBody] UpdateTaxRateRequest request,
        CancellationToken cancellationToken)
    {
        UpdateTaxRateCommand command = new(id, request.Rate);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpPut("{id:guid}/name")]
    public async Task<IActionResult> Rename(
        Guid id,
        [FromBody] RenameTaxRequest request,
        CancellationToken cancellationToken)
    {
        RenameTaxCommand command = new(id, request.Name);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        DeleteTaxCommand command = new(id);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }
}

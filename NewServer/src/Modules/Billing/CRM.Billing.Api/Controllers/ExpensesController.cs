using CRM.Billing.Api.DTOs;
using CRM.Billing.Application.Features.Expenses.Commands.AssignTaxToExpense;
using CRM.Billing.Application.Features.Expenses.Commands.CreateExpense;
using CRM.Billing.Application.Features.Expenses.Commands.DeleteExpense;
using CRM.Billing.Application.Features.Expenses.Commands.UpdateExpenseDetails;
using CRM.Billing.Application.Features.Expenses.Queries.GetExpenseById;
using CRM.Billing.Application.Features.Expenses.Queries.GetExpensesSummary;
using CRM.Billing.Application.Features.Expenses.Queries.GetPagedExpenses;
using CRM.Shared.Abstractions.Results;
using CRM.Shared.Infrastructure.Api;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Billing.Api.Controllers;

[Route("api/v1/expenses")]
public sealed class ExpensesController(ISender sender) : ApiController(sender)
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        GetExpenseByIdQuery query = new(id);
        ExpenseDetailsDto? response = await Sender.Send(query, cancellationToken);

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int limit = 50,
        [FromQuery] int offset = 0,
        [FromQuery] DateOnly? dateFrom = null,
        [FromQuery] DateOnly? dateTo = null,
        CancellationToken cancellationToken = default)
    {
        GetPagedExpensesQuery query = new(limit, offset, dateFrom, dateTo);
        IReadOnlyList<ExpenseListItemDto> response = await Sender.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary(
        [FromQuery] DateOnly dateFrom,
        [FromQuery] DateOnly dateTo,
        CancellationToken cancellationToken)
    {
        GetExpensesSummaryQuery query = new(dateFrom, dateTo);
        IReadOnlyList<ExpenseSummaryDto> response = await Sender.Send(query, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateExpenseCommand command,
        CancellationToken cancellationToken)
    {
        Result result = await Sender.Send(command, cancellationToken);
        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateDetails(
        Guid id,
        [FromBody] UpdateExpenseRequest request,
        CancellationToken cancellationToken)
    {
        UpdateExpenseDetailsCommand command = new(id, request.Date, request.Category, request.Description);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpPatch("{id:guid}/taxes/{taxId:guid}")]
    public async Task<IActionResult> AssignTax(Guid id, Guid taxId, CancellationToken cancellationToken)
    {
        AssignTaxToExpenseCommand command = new(id, taxId);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        DeleteExpenseCommand command = new(id);
        Result result = await Sender.Send(command, cancellationToken);

        return result.IsFailure
            ? HandleFailure(result)
            : Ok();
    }
}

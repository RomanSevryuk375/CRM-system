using CRM.Shared.Abstractions.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Shared.Infrastructure.Api;

[ApiController]
public abstract class ApiController(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;

    protected IActionResult HandleFailure(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Result is success, cannot handle failure.");
        }

        return result.Error.Type switch
        {
            ErrorType.NotFound => NotFound(CreateProblemDetails(
                "Not Found", StatusCodes.Status404NotFound, result.Error)),

            ErrorType.Validation => BadRequest(CreateProblemDetails(
                "Bad Request", StatusCodes.Status400BadRequest, result.Error)),

            ErrorType.Conflict => Conflict(CreateProblemDetails(
                "Conflict", StatusCodes.Status409Conflict, result.Error)),

            _ => StatusCode(StatusCodes.Status500InternalServerError, CreateProblemDetails(
                "Internal Server Error", StatusCodes.Status500InternalServerError, result.Error))
        };
    }

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error)
    {
        return new ProblemDetails
        {
            Title = title,
            Status = status,
            Detail = error.Message,
            Extensions = { { "errorCode", error.Code } }
        };
    }
}
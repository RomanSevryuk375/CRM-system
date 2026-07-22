using Microsoft.AspNetCore.Mvc;

namespace CRM.Shared.Abstractions.Results;

public static class ResultExtensions
{
    public static ActionResult ToActionResult<T>(
        this ControllerBase controller, Result<T> result)
    {
        if (result.IsSuccess)
        {
            return controller.Ok(result.Value);
        }

        return MapError(controller, result.Error);
    }

    public static ActionResult ToActionResult(
        this ControllerBase controller, Result result)
    {
        if (result.IsSuccess)
        {
            return controller.NoContent();
        }

        return MapError(controller, result.Error);
    }

    private static ObjectResult MapError(
        ControllerBase controller, Error error)
    {
        var response = new
        {
            error = error.Code,
            message = error.Message
        };

        return error.Type switch
        {
            ErrorType.NotFound => controller.NotFound(response),
            ErrorType.Validation => controller.BadRequest(response),
            ErrorType.Conflict => controller.Conflict(response),

            _ => controller.StatusCode(500, new { error = "InternalError", message = error.Message })
        };
    }
}
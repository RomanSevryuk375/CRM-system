using CRM.Shared.Abstractions.Results;
using System.Linq.Expressions;
using System.Reflection;

namespace CRM.Shared.Infrastructure.Application.Behaviors;

public static class BehaviorHelpers
{
    public static TResponse CreateFailedResult<TResponse>(Error error)
    {
        return ResultFactory<TResponse>.CreateFailure(error);
    }

    private static class ResultFactory<TResponse>
    {
        public static readonly Func<Error, TResponse> CreateFailure;

#pragma warning disable S3963 
        static ResultFactory()
#pragma warning restore S3963 
        {
            Type responseType = typeof(TResponse);

            if (responseType == typeof(Result))
            {
                CreateFailure = error => (TResponse)(object)Result.Failure(error);
                return;
            }

            if (responseType.IsGenericType &&
                responseType.GetGenericTypeDefinition() == typeof(Result<>))
            {
                Type resultTypeArg = responseType.GetGenericArguments()[0];
                Type genericResultType = typeof(Result<>).MakeGenericType(resultTypeArg);

                MethodInfo? failureMethod = genericResultType.GetMethod(
                    "Failure",
                    BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly,
                    null,
                    [typeof(Error)],
                    null);

                if (failureMethod is null)
                {
#pragma warning disable S3877 
                    throw new InvalidOperationException($"Method 'Failure' not found on {genericResultType.Name}");
#pragma warning restore S3877 
                }

                ParameterExpression errorParam = Expression.Parameter(typeof(Error), "error");
                MethodCallExpression methodCall = Expression.Call(null, failureMethod, errorParam);
                UnaryExpression castExpression = Expression.Convert(methodCall, typeof(TResponse));

                CreateFailure = Expression.Lambda<Func<Error, TResponse>>(
                    castExpression, errorParam).Compile();
            }
            else
            {
                CreateFailure = _ =>
                    throw new UnauthorizedAccessException($"Unsupported response type: {responseType.Name}");
            }
        }
    }
}

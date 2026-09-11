using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;
using System.Reflection;

namespace CRM.Shared.Infrastructure.Data.Converters;

public class StronglyTypedIdConverter<TId, TValue> : ValueConverter<TId, TValue>
{
    public StronglyTypedIdConverter()
        : base(CreateToProviderExpression(), CreateFromProviderExpression())
    {
    }

    private static Expression<Func<TId, TValue>> CreateToProviderExpression()
    {
        ParameterExpression param = Expression.Parameter(typeof(TId), "id");
        MemberExpression member = Expression.PropertyOrField(param, "Id");
        return Expression.Lambda<Func<TId, TValue>>(member, param);
    }

    private static Expression<Func<TValue, TId>> CreateFromProviderExpression()
    {
        ParameterExpression param = Expression.Parameter(typeof(TValue), "value");
        ConstructorInfo ctor = typeof(TId).GetConstructor([typeof(TValue)])
                   ?? throw new InvalidOperationException($"Ctor not found with {typeof(TValue)} into {typeof(TId)}");
        NewExpression newExpr = Expression.New(ctor, param);
        return Expression.Lambda<Func<TValue, TId>>(newExpr, param);
    }
}
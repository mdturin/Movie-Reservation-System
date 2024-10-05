using System.Linq.Expressions;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Application.Conditions;

public class FieldCondition<T>(string fieldName, object value) : ICondition<T>
{
    public string FieldName { get; set; } = fieldName;
    public object Value { get; set; } = value;

    public Expression<Func<T, bool>> ToExpression()
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var member = Expression.PropertyOrField(parameter, FieldName);
        var constant = Expression.Constant(Value);
        var body = Expression.Equal(member, constant);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}


using System.Linq.Expressions;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Application.Conditions;

public class InCondition<T>(string fieldName, IEnumerable<object> values)
    : ICondition<T>
{
    public string FieldName { get; } = fieldName;
    public IEnumerable<object> Values { get; } = values;

    public Expression<Func<T, bool>> ToExpression()
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var field = Expression.PropertyOrField(parameter, FieldName);

        var constant = Expression.Constant(Values);
        var containsMethod = typeof(Enumerable).GetMethods()
            .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
            .MakeGenericMethod(field.Type);

        var body = Expression.Call(containsMethod, constant, field);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }
}

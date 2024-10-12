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

        // This is the key difference: We cast the values list to the appropriate type
        var containsExpression = Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.Contains),
            [field.Type], // Pass the type of the field for generic Contains
            Expression.Constant(Values),
            field
        );

        return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
    }
}

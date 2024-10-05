using System.Linq.Expressions;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Application.Conditions;

public class ContainsCondition<T>(string fieldName, string value, bool caseInsensitive = false) : ICondition<T>
{
    public string FieldName { get; set; } = fieldName;
    public string Value { get; set; } = value;
    public bool CaseInsensitive { get; set; } = caseInsensitive;

    public Expression<Func<T, bool>> ToExpression()
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var field = Expression.PropertyOrField(parameter, FieldName);

        // Convert field to lowercase if case-insensitive comparison is needed
        Expression fieldExpression = field;
        if (CaseInsensitive)
        {
            fieldExpression = Expression
                .Call(field, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
        }

        // Create expression for the value (also lowercased if needed)
        var constantValue = CaseInsensitive ? Value.ToLower() : Value;
        var constant = Expression.Constant(constantValue);

        // Create the Contains method call expression
        var containsMethod = typeof(string).GetMethod("Contains", [typeof(string)]);
        var containsExpression = Expression.Call(fieldExpression, containsMethod, constant);

        return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
    }
}


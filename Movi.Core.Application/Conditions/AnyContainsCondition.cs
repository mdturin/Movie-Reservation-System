using System.Linq.Expressions;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Application.Conditions;

public class AnyContainsCondition<T> : ICondition<T>
{
    public string FieldName { get; set; }
    public IEnumerable<string> Values { get; set; }
    public bool CaseInsensitive { get; set; }

    public AnyContainsCondition(string fieldName, IEnumerable<string> values, bool caseInsensitive = false)
    {
        FieldName = fieldName;
        Values = values;
        CaseInsensitive = caseInsensitive;
    }

    public Expression<Func<T, bool>> ToExpression()
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var field = Expression.PropertyOrField(parameter, FieldName);

        // Create a list of expressions for each value in the collection
        var containsExpressions = new List<Expression>();

        foreach (var value in Values)
        {
            // Convert field to lowercase if case-insensitive comparison is needed
            Expression fieldExpression = field;
            if (CaseInsensitive)
            {
                fieldExpression = Expression.Call(field, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
            }

            // Create expression for the value (also lowercased if needed)
            var constantValue = CaseInsensitive ? value.ToLower() : value;
            var constant = Expression.Constant(constantValue);

            // Create the Contains method call expression
            var containsMethod = typeof(string).GetMethod("Contains", [typeof(string)]);
            var containsExpression = Expression.Call(fieldExpression, containsMethod, constant);

            containsExpressions.Add(containsExpression);
        }

        // Combine all the "Contains" expressions using Expression.OrElse (logical OR)
        var combinedExpression = containsExpressions.Aggregate(Expression.OrElse);

        return Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
    }
}


using System.Linq.Expressions;
using Movi.Core.Domain.Abstractions;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Application.Conditions;

class ReplaceParameterVisitor : ExpressionVisitor
{
    private readonly ParameterExpression _oldParameter;
    private readonly ParameterExpression _newParameter;

    public ReplaceParameterVisitor(
        ParameterExpression oldParameter,
        ParameterExpression newParameter)
    {
        _oldParameter = oldParameter;
        _newParameter = newParameter;
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return node == _oldParameter ? _newParameter : base.VisitParameter(node);
    }
}

public class AndCondition<T>(params ICondition<T>[] conditions)
    : ACompositeCondition<T>(conditions)
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        if (Conditions.Count == 0)
            return (x) => true;

        var parameter = Expression.Parameter(typeof(T), "x");

        var combinedExpressionBody = Conditions
            .Select(c => c.ToExpression())
            .Select(expr => {
                var visitor = new ReplaceParameterVisitor(expr.Parameters[0], parameter);
                return visitor.Visit(expr.Body);
            })
            .Aggregate(Expression.AndAlso);

        return Expression.Lambda<Func<T, bool>>(combinedExpressionBody, parameter);
    }
}


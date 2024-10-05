using System.Linq.Expressions;
using Movi.Core.Domain.Abstractions;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Application.Conditions;

public class OrCondition<T>(params ICondition<T>[] conditions)
    : ACompositeCondition<T>(conditions)
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        var combinedExpression = Conditions
            .Select(c => c.ToExpression())
            .Aggregate((expr1, expr2) =>
                Expression.Lambda<Func<T, bool>>(
                    Expression.OrElse(expr1.Body, expr2.Body),
                    expr1.Parameters.Single()
                ));

        return combinedExpression;
    }
}


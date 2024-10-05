using System.Linq.Expressions;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Domain.Abstractions;

public abstract class ACompositeCondition<T> : ICondition<T>
{
    protected List<ICondition<T>> Conditions { get; } = [];

    public ACompositeCondition(params ICondition<T>[] conditions)
    {
        Conditions.AddRange(conditions);
    }

    public void AddCondition(ICondition<T> condition)
    {
        Conditions.Add(condition);
    }

    public abstract Expression<Func<T, bool>> ToExpression();
}

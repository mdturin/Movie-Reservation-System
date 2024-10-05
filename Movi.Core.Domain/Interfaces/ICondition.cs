using System.Linq.Expressions;

namespace Movi.Core.Domain.Interfaces;

public interface ICondition<T>
{
    Expression<Func<T, bool>> ToExpression();
}

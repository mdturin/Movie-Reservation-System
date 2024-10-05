using System.Linq.Expressions;
using Movi.Core.Domain.Interfaces;

namespace Movi.Core.Application.Conditions;

public class AnyCondition<T, TCollectionItem>(
    string collectionProperty,
    ICondition<TCollectionItem> innerCondition) : ICondition<T>
{
    private string CollectionProperty { get; } = collectionProperty;
    private ICondition<TCollectionItem> InnerCondition { get; } = innerCondition;

    public Expression<Func<T, bool>> ToExpression()
    {
        // Parameter for the main entity (T)
        var parameter = Expression.Parameter(typeof(T), "x");

        // Access the collection property on the main entity
        var collectionProperty = Expression
            .PropertyOrField(parameter, CollectionProperty);

        // Inner condition for the items in the collection
        var innerLambda = InnerCondition.ToExpression();

        // Apply the Any method to the collection
        var anyMethod = typeof(Enumerable).GetMethods()
            .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(TCollectionItem));

        var anyExpression = Expression
            .Call(anyMethod, collectionProperty, innerLambda);

        // Return a lambda expression that applies Any to the collection
        return Expression.Lambda<Func<T, bool>>(anyExpression, parameter);
    }
}


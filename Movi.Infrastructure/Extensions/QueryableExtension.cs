using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Movi.Core.Domain.Interfaces;

namespace Movi.Infrastructure.Extensions;

public static class QueryableExtension
{
    public static IQueryable<T> ApplyIncludes<T>(
        this IQueryable<T> query,
        params Expression<Func<T, object>>[] includes)
            where T : class, IDatabaseModel
    {
        return includes.Aggregate(query, (current, include) => current.Include(include));
    }
}

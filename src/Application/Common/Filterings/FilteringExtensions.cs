using Budgetly.Domain.Common;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Filterings;

public static class FilteringExtensions
{
    public static IQueryable<T> ForCurrentUser<T>(this IQueryable<T> query, string? userId) 
        where T : AuditableEntity => query.Where(w => w.UserId == userId);
}
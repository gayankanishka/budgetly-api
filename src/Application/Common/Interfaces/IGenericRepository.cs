using System.Linq.Expressions;

namespace Budgetly.Application.Common.Interfaces;

public interface IGenericRepository<T>
{
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<T?> FindAsync(Expression<Func<T, bool>> query, CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
}
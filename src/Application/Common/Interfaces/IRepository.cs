using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    void SetFilterStrategy(IFilterStrategy filterStrategy);
    IQueryable<T> GetAll();
    IQueryable<T> GetAll(IFilter filter);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
}
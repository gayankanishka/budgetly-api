using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Interfaces;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<double> GetActualIncomeAsync(DateTimeOffset startDate, DateTimeOffset endDate,
        CancellationToken cancellationToken);

    Task<double> GetActualExpenseAsync(DateTimeOffset startDate, DateTimeOffset endDate,
        CancellationToken cancellationToken);
}
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Infrastructure.Persistence.Repositories;

public class BudgetHistoryRepository : IBudgetHistoryRepository
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTimeService;


    public BudgetHistoryRepository(IApplicationDbContext context, IDateTimeService dateTimeService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
    }

    public IQueryable<BudgetHistory> GetAll()
    {
        return _context.BudgetHistories;
    }

    public IQueryable<BudgetHistory> GetHistoryForPastYear(CancellationToken cancellationToken)
    {
        return GetAll()
            .Where(x => x.Date >= _dateTimeService.OneYearAgoFromNow && x.Date <= _dateTimeService.UtcNow)
            .OrderBy(x => x.Date)
            .AsNoTracking();
    }
}
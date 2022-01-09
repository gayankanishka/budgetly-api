using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Budgets.Queries.GetBudgets;

public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, IEnumerable<BudgetDto>>
{
    private readonly IBudgetRepository _repository;
    private readonly IMapper _mapper;

    public GetBudgetsQueryHandler(IBudgetRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BudgetDto>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll()
            .Include(x => x.BudgetItems)
            .ThenInclude(x => x.TransactionCategory)
            .AsNoTracking()
            .OrderByDescending(x => x.EndDate)
            .Select(x => _mapper.Map<BudgetDto>(x))
            .ToListAsync(cancellationToken);
    }
}
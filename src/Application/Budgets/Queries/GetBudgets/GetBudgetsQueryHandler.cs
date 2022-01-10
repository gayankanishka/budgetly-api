using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Budgets.Queries.GetBudgets;

public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, PagedResponse<BudgetDto>>
{
    private readonly IBudgetRepository _repository;
    private readonly IMapper _mapper;

    public GetBudgetsQueryHandler(IBudgetRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<BudgetDto>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
    {
        int skipAmount = (request.PageNumber - 1) * request.PageSize;

        int totalRecords = await _repository.GetAll()
            .CountAsync(cancellationToken);
        
        var budgets = await _repository.GetAll()
            .Include(x => x.BudgetItems)
            .ThenInclude(x => x.TransactionCategory)
            .Skip(skipAmount)
            .Take(request.PageSize)
            .AsNoTracking()
            .OrderByDescending(x => x.EndDate)
            .Select(x => _mapper.Map<BudgetDto>(x))
            .ToListAsync(cancellationToken);
        
        return new PagedResponse<BudgetDto>(budgets, request.PageNumber, request.PageSize,
            totalRecords);
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Models;
using Budgetly.Application.Mappings;
using Budgetly.Domain.Dtos;
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
        return await _repository.GetAll()
            .Include(x => x.BudgetItems)
            .ThenInclude(x => x.TransactionCategory)
            .OrderByDescending(x => x.EndDate)
            .ProjectTo<BudgetDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
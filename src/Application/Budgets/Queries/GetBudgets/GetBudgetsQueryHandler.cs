using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Filters;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Mappings;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Budgets.Queries.GetBudgets;

public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetsQuery, PagedResponse<BudgetDto>>
{
    private readonly IMapper _mapper;
    private readonly IBudgetRepository _repository;

    public GetBudgetsQueryHandler(IMapper mapper, IBudgetRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<PagedResponse<BudgetDto>> Handle(GetBudgetsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll()
            .Include(x => x.BudgetItems)
            .ThenInclude(x => x.TransactionCategory)
            .ApplyFilters(request)
            .OrderByDescending(x => x.EndDate)
            .ProjectTo<BudgetDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
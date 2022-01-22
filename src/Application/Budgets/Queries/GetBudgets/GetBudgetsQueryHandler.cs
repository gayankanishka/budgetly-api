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
    private readonly IBudgetRepository _repository;
    private readonly IMapper _mapper;

    public GetBudgetsQueryHandler(IBudgetRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PagedResponse<BudgetDto>> Handle(GetBudgetsQuery request,
        CancellationToken cancellationToken)
    {
        _repository.SetFilterStrategy(new GetBudgetsFilterStrategy());

        return await _repository.GetAll(request)
            .Include(x => x.TransactionCategory)
            .OrderByDescending(x => x.Name)
            .ProjectTo<BudgetDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
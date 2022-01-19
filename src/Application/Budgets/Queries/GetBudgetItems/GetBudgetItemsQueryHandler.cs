using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Filters;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Mappings;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Budgets.Queries.GetBudgetItems;

public class GetBudgetItemsQueryHandler : IRequestHandler<GetBudgetItemsQuery, PagedResponse<BudgetItemDto>>
{
    private readonly IBudgetItemRepository _repository;
    private readonly IMapper _mapper;

    public GetBudgetItemsQueryHandler(IBudgetItemRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PagedResponse<BudgetItemDto>> Handle(GetBudgetItemsQuery request,
        CancellationToken cancellationToken)
    {
        _repository.SetFilterStrategy(new GetBudgetItemsFilterStrategy());
        
        return await _repository.GetAll(request)
            .Include(x => x.TransactionCategory)
            .OrderByDescending(x => x.Name)
            .ProjectTo<BudgetItemDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
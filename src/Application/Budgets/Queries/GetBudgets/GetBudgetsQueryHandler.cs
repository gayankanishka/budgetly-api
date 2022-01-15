using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Mappings;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Budgets.Queries.GetBudgets;
public class GetBudgetsQueryHandler : IRequestHandler<GetBudgetItemsQuery, PagedResponse<BudgetItemDto>>
{
    private readonly IMapper _mapper;
    private readonly IBudgetItemRepository _itemRepository;

    public GetBudgetsQueryHandler(IMapper mapper, IBudgetItemRepository itemRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
    }

    public async Task<PagedResponse<BudgetItemDto>> Handle(GetBudgetItemsQuery request, CancellationToken cancellationToken)
    {
        return await _itemRepository.GetAll()
            .Include(x => x.TransactionCategory)
            .OrderByDescending(x => x.Name)
            .ProjectTo<BudgetItemDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
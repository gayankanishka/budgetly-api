using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Budgets.Queries.GetBudgetHistory;

public class GetBudgetHistoryQueryHandler : IRequestHandler<GetBudgetHistoryQuery, IEnumerable<BudgetHistoryDto>>
{
    private readonly IBudgetHistoryRepository _repository;
    private readonly IMapper _mapper;

    public GetBudgetHistoryQueryHandler(IBudgetHistoryRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<BudgetHistoryDto>> Handle(GetBudgetHistoryQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetHistoryForPastYear(cancellationToken)
            .ProjectTo<BudgetHistoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
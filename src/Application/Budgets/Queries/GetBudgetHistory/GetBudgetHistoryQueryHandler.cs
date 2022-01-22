using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Budgets.Queries.GetBudgetHistory;

public class GetBudgetHistoryQueryHandler : IRequestHandler<GetBudgetHistoryQuery, IEnumerable<BudgetHistoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IBudgetHistoryRepository _repository;

    public GetBudgetHistoryQueryHandler(IMapper mapper, IBudgetHistoryRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<BudgetHistoryDto>> Handle(GetBudgetHistoryQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetHistoryForPastYear(cancellationToken)
            .ProjectTo<BudgetHistoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
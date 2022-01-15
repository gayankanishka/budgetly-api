using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Filters;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Mappings;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Budgets.Queries.GetCurrentBudgetStat;
public class GetCurrentBudgetStatQueryHandler : IRequestHandler<GetCurrentBudgetStatQuery, BudgetStatDto>
{
    private readonly IMapper _mapper;
    private readonly IBudgetItemRepository _itemRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetCurrentBudgetStatQueryHandler(IMapper mapper, IBudgetItemRepository itemRepository, ITransactionRepository transactionRepository, ICurrentUserService currentUserService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public async Task<BudgetStatDto> Handle(GetCurrentBudgetStatQuery request, CancellationToken cancellationToken)
    {

        var targetExpense = _itemRepository.GetAll()
            .ForCurrentUser(_currentUserService.UserId)
            .Select(x => x.TargetExpense)
            .Sum();

        var startDate = new DateTime(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month, 1);

        var endDate = new DateTime(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month,
            DateTime.DaysInMonth(DateTimeOffset.UtcNow.Year, DateTimeOffset.UtcNow.Month));

        var actualExpense = _transactionRepository.GetAll()
            .ForCurrentUser(_currentUserService.UserId)
            .Where(x => x.Type == Domain.Enums.TransactionTypes.Expense)
            .Where(x => x.DateTime >= startDate && x.DateTime <= endDate)
            .Select(x => x.Amount)
            .Sum();

        var actualIncome = _transactionRepository.GetAll()
            .ForCurrentUser(_currentUserService.UserId)
            .Where(x => x.Type == Domain.Enums.TransactionTypes.Income)
            .Where(x => x.DateTime >= startDate && x.DateTime <= endDate)
            .Select(x => x.Amount)
            .Sum();

        return new BudgetStatDto()
        {
            TargetExpense = targetExpense,
            ActualExpense = actualExpense,
            AvailableToSpend = actualIncome - actualExpense
        };
    }
}
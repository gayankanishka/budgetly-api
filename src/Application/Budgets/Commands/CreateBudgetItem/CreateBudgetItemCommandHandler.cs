using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Filters;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Budgets.Commands.CreateBudgetItem;

public class CreateBudgetItemCommandHandler : IRequestHandler<CreateBudgetItemCommand,
    BudgetItemDto>
{
    private readonly IMapper _mapper;
    private readonly IBudgetItemRepository _repository;
    private readonly ICurrentUserService _currentUserService;

    public CreateBudgetItemCommandHandler(IMapper mapper, IBudgetItemRepository repository, ICurrentUserService currentUserService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
    }

    public async Task<BudgetItemDto> Handle(CreateBudgetItemCommand request,
        CancellationToken cancellationToken)
    {
        var budgetItem = _mapper.Map<BudgetItem>(request);

        var result = await _repository.GetAll()
            .ForCurrentUser(_currentUserService.UserId)
            .Where(x => x.TransactionCategoryId == request.TransactionCategoryId)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (result != null)
        {
            throw new AlreadyExistsException($"Budget Item already exists with transactionCategoryId: {request.TransactionCategoryId} categoryId");
        }

        await _repository.AddAsync(budgetItem, cancellationToken);
        return _mapper.Map<BudgetItemDto>(budgetItem);
    }
}
using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.Budgets.Commands.CreateBudgetItem;

public class CreateBudgetItemCommandHandler : IRequestHandler<CreateBudgetItemCommand,
    BudgetItemDto>
{
    private readonly IMapper _mapper;
    private readonly IBudgetItemRepository _repository;

    public CreateBudgetItemCommandHandler(IMapper mapper, IBudgetItemRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<BudgetItemDto> Handle(CreateBudgetItemCommand request,
        CancellationToken cancellationToken)
    {
        var budegtItem = _mapper.Map<BudgetItem>(request);

        await _repository.AddAsync(budegtItem, cancellationToken);
        return _mapper.Map<BudgetItemDto>(budegtItem);
    }
}
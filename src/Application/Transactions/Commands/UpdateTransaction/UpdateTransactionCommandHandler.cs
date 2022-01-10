using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.Transactions.Commands.UpdateTransaction;

public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand>
{
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;

    public UpdateTransactionCommandHandler(ITransactionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (transaction == null)
        {
            throw new Exception($"Transaction with id {request.Id} not found");
        }

        transaction = _mapper.Map<Transaction>(request);
        await _repository.UpdateAsync(transaction, cancellationToken);
        
        return Unit.Value;
    }
}
using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _repository;

    public GetTransactionByIdQueryHandler(IMapper mapper, ITransactionRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _repository
            .GetByIdAsync(request.Id, cancellationToken);

        if (transaction == null)
        {
            throw new NotFoundException(nameof(Transaction), request.Id);
        }

        return _mapper.Map<TransactionDto>(transaction);
    }
}
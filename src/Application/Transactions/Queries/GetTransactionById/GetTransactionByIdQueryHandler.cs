using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.Transactions.Queries.GetTransactionById;

public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
{
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;

    public GetTransactionByIdQueryHandler(ITransactionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _repository
            .GetByIdAsync(request.Id, cancellationToken);
        
        return _mapper.Map<TransactionDto>(transaction);
    }
}
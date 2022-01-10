using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Responses;
using MediatR;

namespace Budgetly.Application.Transactions.Queries.GetTransactions;

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, PagedResponse<TransactionDto>>
{
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;

    public GetTransactionsQueryHandler(ITransactionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<TransactionDto>> Handle(GetTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        int totalResults = await _repository.GetResultsCountAsync(request, cancellationToken);
        
        var transactions= await _repository.GetTransactionsAsync(request, cancellationToken);
        var transactionsDto = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        
        return new PagedResponse<TransactionDto>(transactionsDto, request.PageNumber, request.PageSize, totalResults);
    }
}
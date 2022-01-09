using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Responses;
using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        int skipAmount = (request.PageNumber - 1) * request.PageSize;

        int totalRecords = await _repository.GetAll()
            .CountAsync(cancellationToken);
        
        var transactions= await _repository.GetAll()
            .Skip(skipAmount)
            .Take(request.PageSize)
            .AsNoTracking()
            .OrderByDescending(x => x.DateTime)
            .Select(x => _mapper.Map<TransactionDto>(x))
            .ToListAsync(cancellationToken);
        
        return new PagedResponse<TransactionDto>(transactions, request.PageNumber, request.PageSize,
            totalRecords);
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Filterings;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Mappings;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Transactions.Queries.GetTransactions;

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, PagedResponse<TransactionDto>>
{
    private readonly ITransactionRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _user;

    public GetTransactionsQueryHandler(ITransactionRepository repository, IMapper mapper, ICurrentUserService user)
    {
        _repository = repository;
        _mapper = mapper;
        _user = user;
    }

    public async Task<PagedResponse<TransactionDto>> Handle(GetTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAll()
            .Include(x => x.Category)
            .ForCurrentUser(_user.UserId)
            .ApplyFilters(request)
            .OrderByDescending(x => x.DateTime)
            .ProjectTo<TransactionDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
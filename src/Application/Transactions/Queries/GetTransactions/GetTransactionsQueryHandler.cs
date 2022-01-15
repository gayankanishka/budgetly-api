using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Filters;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Mappings;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.Transactions.Queries.GetTransactions;

public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, PagedResponse<TransactionDto>>
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _repository;
    private readonly ICurrentUserService _user;

    public GetTransactionsQueryHandler(IMapper mapper, ITransactionRepository repository, ICurrentUserService user)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _user = user ?? throw new ArgumentNullException(nameof(user));
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
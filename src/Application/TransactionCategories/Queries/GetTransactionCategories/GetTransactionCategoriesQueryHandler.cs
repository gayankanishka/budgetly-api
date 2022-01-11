using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Filterings;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Mappings;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;

public class GetTransactionCategoriesQueryHandler : IRequestHandler<GetTransactionCategoriesQuery,
    PagedResponse<TransactionCategoryDto>>
{
    private readonly ITransactionCategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _user;

    public GetTransactionCategoriesQueryHandler(ITransactionCategoryRepository repository, IMapper mapper, ICurrentUserService user)
    {
        _repository = repository;
        _mapper = mapper;
        _user = user;
    }

    public async Task<PagedResponse<TransactionCategoryDto>> Handle(GetTransactionCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        return await _repository.GetAll()
            .ForCurrentUser(_user.UserId)
            .OrderBy(x => x.Name)
            .ProjectTo<TransactionCategoryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
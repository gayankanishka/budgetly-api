using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Filters;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Mappings;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;

public class GetTransactionCategoriesQueryHandler : IRequestHandler<GetTransactionCategoriesQuery,
    PagedResponse<TransactionCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly ITransactionCategoryRepository _repository;
    private readonly ICurrentUserService _user;

    public GetTransactionCategoriesQueryHandler(IMapper mapper, ITransactionCategoryRepository repository,
        ICurrentUserService user)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _user = user ?? throw new ArgumentNullException(nameof(user));
    }

    public async Task<PagedResponse<TransactionCategoryDto>> Handle(GetTransactionCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAll()
            .Where(x => x.UserId == _user.UserId || x.IsPreset == true)
            .ApplyFilters(request)
            .OrderBy(x => x.Name)
            .ProjectTo<TransactionCategoryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
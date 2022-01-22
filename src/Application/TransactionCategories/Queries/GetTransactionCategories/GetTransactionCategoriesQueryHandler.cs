using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Filters;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Mappings;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;

public class GetTransactionCategoriesQueryHandler : IRequestHandler<GetTransactionCategoriesQuery,
    PagedResponse<TransactionCategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly ITransactionCategoryRepository _repository;

    public GetTransactionCategoriesQueryHandler(IMapper mapper, ITransactionCategoryRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<PagedResponse<TransactionCategoryDto>> Handle(GetTransactionCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        _repository.SetFilterStrategy(new GetTransactionCategoriesFilterStrategy());

        return await _repository.GetAll(request)
            .OrderBy(x => x.Name)
            .ProjectTo<TransactionCategoryDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
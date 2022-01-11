using AutoMapper;
using AutoMapper.QueryableExtensions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Application.Common.Models;
using Budgetly.Application.Mappings;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;

public class GetTransactionCategoriesQueryHandler : IRequestHandler<GetTransactionCategoriesQuery,
    PagedResponse<TransactionCategoryDto>>
{
    private readonly ITransactionCategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetTransactionCategoriesQueryHandler(ITransactionCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PagedResponse<TransactionCategoryDto>> Handle(GetTransactionCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        return await _repository.GetAll()
            .OrderBy(x => x.Name)
            .ProjectTo<TransactionCategoryDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
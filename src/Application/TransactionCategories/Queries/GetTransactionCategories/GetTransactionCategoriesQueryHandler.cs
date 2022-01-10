using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        int skipAmount = (request.PageNumber - 1) * request.PageSize;

        int totalRecords = await _repository.GetAll()
            .CountAsync(cancellationToken);
        
        var categories = await _repository.GetAll()
            .Skip(skipAmount)
            .Take(request.PageSize)
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => _mapper.Map<TransactionCategoryDto>(x))
            .ToListAsync(cancellationToken);
        
       return new PagedResponse<TransactionCategoryDto>(categories, request.PageNumber, request.PageSize,
           totalRecords);
    }
}
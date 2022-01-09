using AutoMapper;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;

public class GetTransactionCategoriesQueryHandler : IRequestHandler<GetTransactionCategoriesQuery,
    IEnumerable<TransactionCategoryDto>>
{
    private readonly ITransactionCategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetTransactionCategoriesQueryHandler(ITransactionCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TransactionCategoryDto>> Handle(GetTransactionCategoriesQuery request, 
        CancellationToken cancellationToken)
    {
        return await _repository.GetAll()
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(x => _mapper.Map<TransactionCategoryDto>(x))
            .ToListAsync(cancellationToken);
    }
}
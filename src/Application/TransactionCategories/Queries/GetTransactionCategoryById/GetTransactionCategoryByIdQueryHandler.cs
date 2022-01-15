using AutoMapper;
using Budgetly.Application.Common.Exceptions;
using Budgetly.Application.Common.Interfaces;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Queries.GetTransactionCategoryById;

public class GetTransactionCategoryByIdQueryHandler : IRequestHandler<GetTransactionCategoryByIdQuery,
    TransactionCategoryDto>
{
    private readonly ITransactionCategoryRepository _repository;
    private readonly IMapper _mapper;

    public GetTransactionCategoryByIdQueryHandler(ITransactionCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<TransactionCategoryDto> Handle(GetTransactionCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var transactionCategory = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (transactionCategory == null)
        {
            throw new NotFoundException(nameof(TransactionCategory), request.Id);
        }

        return _mapper.Map<TransactionCategoryDto>(transactionCategory);
    }
}
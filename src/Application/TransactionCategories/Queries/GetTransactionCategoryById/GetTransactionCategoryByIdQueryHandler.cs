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
    private readonly IMapper _mapper;
    private readonly ITransactionCategoryRepository _repository;

    public GetTransactionCategoryByIdQueryHandler(IMapper mapper, ITransactionCategoryRepository repository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
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
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.DeleteTransactionCategory;

public class DeleteTransactionCategoryCommand : IRequest
{
    public int Id { get; set; }
}
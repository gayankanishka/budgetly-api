using System.ComponentModel.DataAnnotations;
using Budgetly.Domain.Dtos;
using MediatR;

namespace Budgetly.Application.TransactionCategories.Commands.CreateTransactionCategory;

public class CreatTransactionCategoryCommand : IRequest<TransactionCategoryDto>
{
    [Required]
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsPreset { get; set; }
}
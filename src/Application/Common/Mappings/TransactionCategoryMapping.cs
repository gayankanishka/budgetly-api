using AutoMapper;
using Budgetly.Application.TransactionCategories.Commands.CreateTransactionCategory;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Mappings;

public class TransactionCategoryMapping : Profile
{
    public TransactionCategoryMapping()
    {
        CreateMap<TransactionCategory, TransactionCategoryDto>();
        CreateMap<CreateTransactionCategoryCommand, TransactionCategory>();
    }
}
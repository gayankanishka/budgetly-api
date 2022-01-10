using AutoMapper;
using Budgetly.Application.TransactionCategories.Commands.CreateTransactionCategory;
using Budgetly.Application.TransactionCategories.Commands.UpdateTransactionCategory;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Mappings;

public class TransactionCategoryMapping : Profile
{
    public TransactionCategoryMapping()
    {
        CreateMap<TransactionCategory, TransactionCategoryDto>();
        CreateMap<CreatTransactionCategoryCommand, TransactionCategory>();
        CreateMap<UpdateTransactionCategoryCommand, TransactionCategoryDto>();
    }
}
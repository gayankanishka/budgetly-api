using AutoMapper;
using Budgetly.Application.TransactionCategories.Commands.CreateTransactionCategory;
using Budgetly.Application.TransactionCategories.Commands.UpdateTransactionCategory;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.TransactionCategories;

public class TransactionCategoryMappings : Profile
{
    public TransactionCategoryMappings()
    {
        CreateMap<TransactionCategory, TransactionCategoryDto>();
        CreateMap<CreatTransactionCategoryCommand, TransactionCategory>();
        CreateMap<UpdateTransactionCategoryCommand, TransactionCategoryDto>();
    }
}
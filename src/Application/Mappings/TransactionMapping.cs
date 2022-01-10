using AutoMapper;
using Budgetly.Application.Transactions.Commands.CreateTransaction;
using Budgetly.Application.Transactions.Commands.UpdateTransaction;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Mappings;

public class TransactionMapping : Profile
{
    public TransactionMapping()
    {
        CreateMap<Transaction, TransactionDto>();
        CreateMap<TransactionCategory, TransactionCategoryDto>();
        CreateMap<CreateTransactionCommand, Transaction>();
    }
}
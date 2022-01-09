using AutoMapper;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Transactions;

public class TransactionMappings : Profile
{
    public TransactionMappings()
    {
        CreateMap<Transaction, TransactionDto>();
    }
}
using AutoMapper;
using Budgetly.Application.Budgets.Commands.CreateBudgetItem;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Mappings;

public class BudgetMapping : Profile
{
    public BudgetMapping()
    {
        CreateMap<BudgetItem, BudgetItemDto>();
        CreateMap<CreateBudgetItemCommand, BudgetItem>();
    }
}
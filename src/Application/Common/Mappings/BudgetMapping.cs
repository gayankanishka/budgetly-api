using AutoMapper;
using Budgetly.Application.Budgets.Commands.CreateBudget;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Mappings;

public class BudgetMapping : Profile
{
    public BudgetMapping()
    {
        CreateMap<Budget, BudgetDto>();
        CreateMap<CreateBudgetCommand, Budget>();
    }
}
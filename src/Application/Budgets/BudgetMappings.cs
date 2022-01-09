using AutoMapper;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Budgets;

public class BudgetMappings : Profile
{
    public BudgetMappings()
    {
        CreateMap<Budget, BudgetDto>();
    }
}
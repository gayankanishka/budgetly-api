using AutoMapper;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Mappings;

public class BudgetMapping : Profile
{
    public BudgetMapping()
    {
        CreateMap<Budget, BudgetDto>();
        CreateMap<BudgetItem, BudgetItemDto>();
    }
}
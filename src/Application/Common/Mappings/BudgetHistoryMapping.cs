using AutoMapper;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Entities;

namespace Budgetly.Application.Common.Mappings;

public class BudgetHistoryMapping : Profile
{
    public BudgetHistoryMapping()
    {
        CreateMap<BudgetHistory, BudgetHistoryDto>();
    }
}
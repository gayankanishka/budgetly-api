using Budgetly.Domain.Common;
using Budgetly.Domain.Entities;

namespace Budgetly.Domain.Events;

public class BudgetCreatedEvent : DomainEvent
{
    public BudgetCreatedEvent(Budget budget)
    {
        Budget = budget;
    }

    public Budget Budget { get; }
}
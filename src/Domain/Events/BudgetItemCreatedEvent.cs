using Budgetly.Domain.Common;
using Budgetly.Domain.Entities;

namespace Budgetly.Domain.Events;

public class BudgetItemCreatedEvent : DomainEvent
{
    public BudgetItemCreatedEvent(BudgetItem budgetItem)
    {
        BudgetItem = budgetItem;
    }

    public BudgetItem BudgetItem { get; }
}
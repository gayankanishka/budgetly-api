using Budgetly.Domain.Common;
using Budgetly.Domain.Entities;

namespace Budgetly.Domain.Events;

/// <summary>
///     Event representing a new transaction being deleted.
/// </summary>
public class TransactionDeletedEvent : DomainEvent
{
    /// <summary>
    ///     The constructor for the event.
    /// </summary>
    /// <param name="transaction">Newly created transaction.</param>
    public TransactionDeletedEvent(Transaction transaction)
    {
        Transaction = transaction;
    }

    /// <summary>
    ///     Represents the transaction that was deleted.
    /// </summary>
    public Transaction Transaction { get; }
}
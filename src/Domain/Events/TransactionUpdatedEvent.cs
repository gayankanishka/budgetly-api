using Budgetly.Domain.Common;
using Budgetly.Domain.Entities;

namespace Budgetly.Domain.Events;

/// <summary>
///     Event representing a transaction being updated.
/// </summary>
public class TransactionUpdatedEvent : DomainEvent
{
    /// <summary>
    ///     The constructor for the event.
    /// </summary>
    /// <param name="transaction">Newly created transaction.</param>
    public TransactionUpdatedEvent(Transaction transaction)
    {
        Transaction = transaction;
    }

    /// <summary>
    ///     Represents the transaction that was updated.
    /// </summary>
    public Transaction Transaction { get; }
}
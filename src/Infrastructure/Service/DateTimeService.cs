using Budgetly.Application.Common.Interfaces;

namespace Budgetly.Infrastructure.Service;

/// <summary>
///     Provides the datetime required by the application.
/// </summary>
public class DateTimeService : IDateTimeService
{
    /// <summary>
    ///     Datetime offset of the current UTC time.
    /// </summary>
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

    /// <summary>
    ///     First day of the current month.
    /// </summary>
    public DateTimeOffset FirstDayOfCurrentMonth => new DateTime(UtcNow.Year, UtcNow.Month, 1);

    /// <summary>
    ///     Last day of the current month.
    /// </summary>
    public DateTimeOffset LastDayOfCurrentMonth => new DateTime(UtcNow.Year, UtcNow.Month,
        DateTime.DaysInMonth(UtcNow.Year, UtcNow.Month));
}
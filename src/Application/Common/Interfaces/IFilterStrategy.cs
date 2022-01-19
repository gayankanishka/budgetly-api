namespace Budgetly.Application.Common.Interfaces;

public interface IFilterStrategy
{
    object Filter(object query, object filters);
}
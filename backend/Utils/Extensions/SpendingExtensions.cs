using ExpenseControlApplication.Data.Entities;

namespace ExpenseControlApplication.Utils.Extensions;

public class FilterType
{
    public string filter { get; init; }
}
public static class SpendingExtensions
{
    public static IQueryable<Spending> FilterBy(this IQueryable<Spending> spendings, FilterType filterType, string? filterValue)
    {
        return filterType.filter switch
        {
            "ItemName" => spendings = !string.IsNullOrWhiteSpace(filterValue)
                ? spendings.Where(s => s.ItemBought.ToLower().Contains(filterValue.ToLower()))
                : spendings,
            "Description" =>
                spendings = !string.IsNullOrWhiteSpace(filterValue)
                    ? spendings
                        .Where(s => !string.IsNullOrWhiteSpace(s.Description))
                        .Where(s => s.Description!.ToLower().Contains(filterValue.ToLower()))
                    : spendings,
            _ => spendings
        };
    }
    public static IQueryable<Spending> SortSpendings(this IQueryable<Spending> spendings, string? sortBy, bool isDescending)
    {
        return sortBy?.ToLower() switch
        {
            "id" => isDescending
                ? spendings.OrderByDescending(s => s.Id)
                : spendings.OrderBy(s => s.Id),
            "description" => isDescending
                ? spendings.OrderByDescending(s => s.Description)
                : spendings.OrderBy(s => s.Description),
            "item name" => isDescending
                ? spendings.OrderByDescending(s => s.ItemBought)
                : spendings.OrderBy(s => s.ItemBought),
            "value spent" => isDescending
                ? spendings.OrderByDescending(s => s.Description)
                : spendings.OrderBy(s => s.Description),
            _ => spendings
        };   
    }
}
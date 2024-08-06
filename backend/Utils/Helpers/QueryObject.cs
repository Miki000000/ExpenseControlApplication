using System.ComponentModel.DataAnnotations;

namespace ExpenseControlApplication.Utils.Helpers;

public class QueryObject
{
    public string? Description { get; set; }
    public string? ItemName { get; set; }
    [AllowedValues(["Id", "Description", "Item Name", "Value Spent", null], ErrorMessage = 
        "Valid values are: Id, Description, Item Name and Value Spent"
    )]
    public string? SortBy { get; set; }

    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}
namespace ExpenseControlApplication.Data.Factories;

public record struct Status
{
    public bool success { get; set; } 
    public string? message { get; set; }
}

public static class StatusFactory
{
    public static Status CreateStatus(bool success, string? message)
    {
        return new Status { success = success, message = message };
    }
    public static Status CreateStatus(bool success)
    {
        return new Status { success = success };
    }
}
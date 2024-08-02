namespace ExpenseControlApplication.Utils.Exceptions;

public class NotFoundException(string property) : Exception($"{property} not found in the database!");
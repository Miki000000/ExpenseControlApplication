namespace ExpenseControlApplication.Utils.Exceptions;

public class InvalidEntryException(string property) 
    : Exception($"Invalid {property}!");

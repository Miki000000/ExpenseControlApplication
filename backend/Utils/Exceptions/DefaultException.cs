namespace ExpenseControlApplication.Utils.Exceptions;

public class DefaultException(string property): Exception($"{property}");
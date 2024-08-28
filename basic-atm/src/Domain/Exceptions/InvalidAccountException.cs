namespace ATM.Domain.Exceptions;

public class InvalidAccountException(string? message) : Exception(message);
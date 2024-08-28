namespace ATM.Domain.Exceptions;

public class InvalidTransactionException(string? message) : Exception(message);
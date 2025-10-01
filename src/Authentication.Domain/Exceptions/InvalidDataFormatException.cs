namespace Authentication.Domain.Exceptions;

public class InvalidDataFormatException(string message) : Exception(message);
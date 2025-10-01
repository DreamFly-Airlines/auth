namespace Authentication.Application.Exceptions;

public class InvalidCredentialsException(string message) : Exception(message);
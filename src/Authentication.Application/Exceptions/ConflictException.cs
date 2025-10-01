namespace Authentication.Application.Exceptions;

public class ConflictException(string message, EntityStateInfo? entityStateInfo = null) : Exception(message)
{
    public EntityStateInfo? EntityStateInfo { get; } = entityStateInfo;
}
namespace Authentication.Application.Exceptions;

public class NotFoundException(
    string entityName,
    EntityStateInfo? entityStateInfo = null) : Exception($"{entityName} not found")
{
    public EntityStateInfo? EntityStateInfo { get; } =  entityStateInfo;
}
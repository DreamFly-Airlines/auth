using Authentication.Domain.Exceptions;

namespace Authentication.Domain.ValueObjects;

public class JwtOptions
{
    public string Key { get; }
    public TimeSpan ExpiresIn { get; }

    public JwtOptions(string key, TimeSpan expiresIn)
    {
        if (key.Length == 0)
            throw new InvalidDataFormatException("Key must be non-empty");
        Key = key;
        ExpiresIn = expiresIn;
    }
}
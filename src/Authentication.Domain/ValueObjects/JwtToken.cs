using Authentication.Domain.Exceptions;

namespace Authentication.Domain.ValueObjects;

public readonly record struct JwtToken
{
    public string AccessToken { get; }
    public TimeSpan ExpiresIn { get; }
    
    private JwtToken(string accessToken, TimeSpan expiresIn) 
        => (AccessToken, ExpiresIn) = (accessToken, expiresIn);

    public static JwtToken FromString(string accessToken, TimeSpan expiresIn)
    {
        var split = accessToken.Split('.');
        if (split.Length != 3)
            throw new InvalidDataFormatException("A JWT token should have three parts separated by dots");
        if (split[0].Length == 0 || split[1].Length == 0 || split[2].Length == 0)
            throw new InvalidDataFormatException("Each part in JWT token must be non-empty");
        return new(accessToken, expiresIn);
    }
}
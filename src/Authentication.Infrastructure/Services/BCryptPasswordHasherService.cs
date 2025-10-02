using Authentication.Application.Services;

namespace Authentication.Infrastructure.Services;

public class BCryptPasswordHasherService : IPasswordHasherService
{
    public string Hash(string @string) 
        => BCrypt.Net.BCrypt.EnhancedHashPassword(@string);

    public bool Verify(string @string, string hash) 
        => BCrypt.Net.BCrypt.EnhancedVerify(@string, hash);
}
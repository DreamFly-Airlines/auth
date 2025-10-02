namespace Authentication.Application.Services;

public interface IPasswordHasherService
{
    public string Hash(string @string);

    public bool Verify(string @string, string hash);
}
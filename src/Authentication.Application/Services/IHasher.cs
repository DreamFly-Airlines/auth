namespace Authentication.Application.Services;

public interface IHasher
{
    public string Hash(string @string);

    public bool Verify(string @string, string hash);
}
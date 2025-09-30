namespace Authentication.Domain.Entities;

public class User
{
    public string Id { get; }
    public string Login { get; }
    public string PasswordHash { get; }

    public User(string id, string login, string passwordHash)
    {
        Id = id;
        Login = login;
        PasswordHash = passwordHash;
    }
}
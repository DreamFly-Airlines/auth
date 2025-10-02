using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;

namespace Authentication.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly Dictionary<string, User> _users = new();
    public Task<User?> GetByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        _users.TryGetValue(login, out var user);
        return Task.FromResult(user);
    }

    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        if (!_users.TryAdd(user.Login, user))
            throw new Exception();
        return Task.CompletedTask;
    }

    public Task RemoveAsync(User user, CancellationToken cancellationToken = default)
    {
        _users.Remove(user.Login);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
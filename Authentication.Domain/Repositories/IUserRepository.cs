using Authentication.Domain.Entities;

namespace Authentication.Domain.Repositories;

public interface IUserRepository
{
    public Task<User?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    
    public Task AddAsync(User user, CancellationToken cancellationToken = default);
    
    public Task RemoveAsync(User user, CancellationToken cancellationToken = default);
    
    public Task UpdateAsync(User user, CancellationToken cancellationToken = default);
}
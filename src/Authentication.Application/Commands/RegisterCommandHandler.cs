using Authentication.Application.Exceptions;
using Authentication.Application.Services;
using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Shared.Abstractions.Commands;

namespace Authentication.Application.Commands;

public class RegisterCommandHandler(
    IHasher hasher,
    IUserRepository userRepository) : ICommandHandler<RegisterCommand>
{
    public async Task HandleAsync(RegisterCommand command, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByLoginAsync(command.Login, cancellationToken);
        if (user is not null)
        {
            var state = new EntityStateInfo(nameof(User), (nameof(User.Login), user.Login));
            throw new ConflictException("User with such login already exists", state);
        }

        var passwordHash = hasher.Hash(command.Password);
        var id = Guid.NewGuid().ToString();
        var createdUser = new User(id, command.Login, passwordHash);
        await userRepository.AddAsync(createdUser, cancellationToken);
    }
}
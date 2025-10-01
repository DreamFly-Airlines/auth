using Authentication.Application.Exceptions;
using Authentication.Application.Services;
using Authentication.Domain.Entities;
using Authentication.Domain.Repositories;
using Authentication.Domain.ValueObjects;
using Shared.Abstractions.Queries;

namespace Authentication.Application.Queries;

public class LogInQueryHandler(
    IHasher hasher,
    IUserRepository userRepository,
    IJwtProvider jwtProvider) : IQueryHandler<LogInQuery, JwtToken>
{
    public async Task<JwtToken> HandleAsync(LogInQuery query, CancellationToken cancellationToken = default)
    {
        var user = await userRepository.GetByLoginAsync(query.Login, cancellationToken);
        if (user is null)
        {
            var state = new EntityStateInfo(nameof(User), (nameof(User.Login), query.Login));
            throw new NotFoundException(query.Login, state);
        }
        if (!hasher.Verify(query.Password, user.PasswordHash))
            throw new InvalidCredentialsException(query.Login);
        var jwt = jwtProvider.Generate(user);
        return jwt;
    }
}
using Authentication.Domain.ValueObjects;
using Shared.Abstractions.Queries;

namespace Authentication.Application.Queries;

public record LogInQuery(string Login, string Password) : IQuery<JwtToken>;
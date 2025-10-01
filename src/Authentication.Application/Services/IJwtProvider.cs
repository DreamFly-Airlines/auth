using Authentication.Domain.Entities;
using Authentication.Domain.ValueObjects;

namespace Authentication.Application.Services;

public interface IJwtProvider
{
    public JwtToken Generate(User user);
}
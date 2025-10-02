using Authentication.Domain.Entities;
using Authentication.Domain.ValueObjects;

namespace Authentication.Application.Services;

public interface IJwtProviderService
{
    public JwtToken Generate(User user);
}
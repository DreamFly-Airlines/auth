using Shared.Abstractions.Commands;

namespace Authentication.Application.Commands;

public record RegisterCommand(string Login, string Password) : ICommand;
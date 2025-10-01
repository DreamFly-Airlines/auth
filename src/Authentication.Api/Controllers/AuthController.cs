using Authentication.Api.Requests;
using Authentication.Application.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.Abstractions.Commands;

namespace Authentication.Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController(ICommandSender commandSender) : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var registerCommand = new RegisterCommand(request.Login, request.Password);
        await commandSender.SendAsync(registerCommand);
        return Created();
    }

    [HttpPost("login")]
    public IActionResult LogIn([FromBody] LogInRequest request)
    {
        throw new NotImplementedException();
    }
}
using Authentication.Api.Requests;
using Authentication.Api.Responses;
using Authentication.Application.Commands;
using Authentication.Application.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared.Abstractions.Commands;
using Shared.Abstractions.Queries;

namespace Authentication.Api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController(
    IQuerySender querySender,
    ICommandSender commandSender) : Controller
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var registerCommand = new RegisterCommand(request.Login, request.Password);
        await commandSender.SendAsync(registerCommand);
        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogIn([FromBody] LogInRequest request)
    {
        var query = new LogInQuery(request.Login, request.Password);
        var jwt = await querySender.SendAsync(query);
        var response = new LogInResponse
        {
            AccessToken = jwt.Token,
            ExpiresIn = ((DateTimeOffset)jwt.ExpiresIn).ToUnixTimeSeconds(),
            TokenType = "Bearer"
        };
        return Ok(response);
    }
}
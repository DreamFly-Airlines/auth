using Authentication.Api.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController
{
    [HttpGet("{id}")]
    public IActionResult GetUser([FromRoute] string id)
    {
        throw new NotImplementedException();
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    [HttpPost("login")]
    public IActionResult LogIn([FromBody] LogInRequest request)
    {
        throw new NotImplementedException();
    }
}
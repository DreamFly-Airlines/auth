using System.ComponentModel.DataAnnotations;

namespace Authentication.Api.Responses;

public class LogInResponse
{
    public required string AccessToken { get; set; }
    public required string TokenType { get; set; }
    public required long ExpiresIn { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Authentication.Api.Requests;

public class LogInRequest
{
    [Required]
    public required string Login { get; set; }

    [Required]
    public required string Password { get; set; }
}
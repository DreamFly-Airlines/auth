using System.ComponentModel.DataAnnotations;

namespace Authentication.Api.Requests;

public class RegisterRequest
{
    [Required]
    public required string Login { get; set; }
    
    [Required]
    public required string Password { get; set; }
}
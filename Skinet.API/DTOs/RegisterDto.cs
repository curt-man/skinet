using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Skinet.API.DTOs;

public class RegisterDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = string.Empty;

}

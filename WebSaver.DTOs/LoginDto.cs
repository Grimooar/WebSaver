using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class LoginDto
{
    public string Login { get; set; }
    
    public string Email { get; set; }
    public string Password { get; set; }
}
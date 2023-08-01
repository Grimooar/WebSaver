using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class UserCreateDto
{
    public string UserName { get; set; } = "";
   
    public string Name { get; set; } = "";
   
    public string LastName { get; set; } = "";
 
    public string Email { get; set; } = "";
    
    public string PhoneNumber { get; set; } = "";
    
    public string Password { get; set; } = "";
}
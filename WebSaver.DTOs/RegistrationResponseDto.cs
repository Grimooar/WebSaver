namespace WebApplication1.DTOs;

public class RegistrationResponseDto
{
    public bool IsSuccessfulRegistraion { get; set; }
    public IEnumerable<string> Errors { get; set; }
}
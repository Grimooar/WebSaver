using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Kirel.Identity.Core.Models;

using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthOptions _authOptions;
     
        private readonly AuthService _authService;

        public AuthController(IOptions<AuthOptions> authOptions, AuthService authService)
        {
            _authOptions = authOptions.Value;
            
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserCreateDto userDto)
        {
            var result = await _authService.Register(userDto);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _authService.Login(loginDto);

            if (token == null)
            {
                return BadRequest(new { message = "Invalid email or password" });
            }

            return Ok(new { token });
        }
    }
}
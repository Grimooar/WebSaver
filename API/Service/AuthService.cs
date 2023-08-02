using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Service
{
    public class AuthService
    {
        private readonly AuthOptions _authOptions;
        private readonly UserManager<User> _userManager;
       
        private readonly IConfiguration _configuration;
        private readonly JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        public AuthService(IOptions<AuthOptions> authOptions, UserManager<User> userManager,IConfiguration configuration)
        {
            _authOptions = authOptions.Value;
            _userManager = userManager;
           
            _configuration = configuration;
        }
    public async Task<string> Login(LoginDto loginDto)
{
    // Check if the input is a valid email
    bool isEmail = new EmailAddressAttribute().IsValid(loginDto.Login);

    // Find the user by login (username or email) based on the input
    var user = isEmail
        ? await _userManager.FindByEmailAsync(loginDto.Login)
        : await _userManager.FindByNameAsync(loginDto.Login);

    if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
    {
        // User not found or invalid password
        return null;
    }

    // The token is valid, you can continue with the login logic and generating a new token as needed.

    // Create a list of claims to include in the new JWT
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Name, user.UserName)
        // Add any additional claims you want to include in the token.
    };

    // Generate a new JWT using the provided authentication options (similar to your original code)
    var authOptions = _configuration.GetSection("AuthOptions").Get<AuthOptions>();
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions?.Key ?? string.Empty));
    var token = new JwtSecurityToken(
        authOptions.Issuer,
        authOptions.Audience,
        claims,
        expires: DateTime.UtcNow.AddDays(authOptions.Lifetime),
        signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenString = tokenHandler.WriteToken(token);
    Console.WriteLine($"Generated token: {tokenString}");

    // Validate the incoming token
    bool isTokenValid = IsTokenValid(tokenString);

    if (isTokenValid)
    {
        // The token is valid. You can continue with further processing or authentication logic.
        Console.WriteLine("Everything is ok");
    }
    else
    {
        Console.WriteLine("You are not authorized");
        // The token is invalid. Handle the error or reject the request.
    }

    // Return the new token as a string
    return tokenString;
}



        // public async Task<string?> Login(LoginDto loginDto)
        // {
        //    //var user = await _userService.GetUserByEmailAsync(loginDto.Email);
        //      var user = await _userManager.FindByEmailAsync(loginDto.Email);
        //     if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
        //         return null;
        //
        //     var claims = new List<Claim>
        //     {
        //         new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //         new Claim(ClaimTypes.Email, user.Email),
        //         new Claim(ClaimTypes.Name, user.UserName)
        //     };
        //
        //     var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key));
        //     var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //     var token = new JwtSecurityToken(_authOptions.Issuer, _authOptions.Audience, claims,
        //         expires: DateTime.Now.AddMinutes(Convert.ToDouble(_authOptions.Lifetime)), signingCredentials: credentials);
        //
        //     return new JwtSecurityTokenHandler().WriteToken(token);
        // }

        private ClaimsPrincipal ValidateJwtToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var authOptions = _configuration.GetSection("AuthOptions").Get<AuthOptions>();
            // Configure the TokenValidationParameters based on your requirements
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = authOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = authOptions.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Key)),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // Ensure the token is strictly valid at the time of validation
            };

            try
            {
                // Validate the JWT token using the provided parameters
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
                return claimsPrincipal;
            }
            catch (SecurityTokenException)
            {
                // Token validation failed, return null or handle the error accordingly
                return null;
            }
            catch (Exception)
            {
                // Other exceptions may be thrown for various reasons
                // Handle them appropriately based on your application's requirements
                return null;
            }
        }
        public bool IsTokenValid(string jwtToken)
        {
            // Use the ValidateJwtToken method to validate the token
            var principal = ValidateJwtToken(jwtToken);
            return principal != null;
        }
        public async Task<IdentityResult> Register(UserCreateDto userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Name = userDto.Name,
                LastName = userDto.LastName,
                Created = DateTime.UtcNow,
                Email = userDto.Email
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);

            return result;
        }

    }
}
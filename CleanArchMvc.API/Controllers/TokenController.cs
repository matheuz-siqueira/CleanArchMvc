using CleanArchMvc.Domain.Account; 
using CleanArchMvc.API.Models;

using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims; 
using System.Text;

using Microsoft.IdentityModel.Tokens;

namespace CleanArchMvc.API.Controllers;

[Route("api/token")]
public class TokenController : ControllerBase
{
    private readonly IAuthenticate _authService;    
    private readonly IConfiguration _configuration;
    public TokenController(IAuthenticate authService, IConfiguration configuration)
    {
        _authService = authService; 
        _configuration = configuration; 
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel request)
    {
        var result = await _authService.Authenticate(request.Email, request.Password);
        if(result)
        {
            return GenerateToken(request);
        }
        else 
        {
            ModelState.AddModelError(string.Empty, "Invalid Login attempt");
            return BadRequest(ModelState);
        }
    }

    [HttpPost("register")]
    [ApiExplorerSettings(IgnoreApi = false)]
    public async Task<ActionResult> Create([FromBody] RegisterModel request)
    {
        if(request is null)
        {
            return BadRequest(new { message = "Invalid data."});
        }
        var result = await _authService.RegisterUser(request.Email, request.Password);
        if(result)
        {
            return Ok(); 
        }
        else 
        {
            ModelState.AddModelError(string.Empty, "Invalid register attempt.");
            return BadRequest(ModelState);
        }
    }


    private UserToken GenerateToken(LoginModel request)
    {
        var claims = new List<Claim>(); 
        claims.Add(new Claim(ClaimTypes.Name, request.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        var JWTkey = Encoding.ASCII.GetBytes(_configuration["JWT:KEY"]);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(JWTkey),
            SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.UtcNow.AddMinutes(10);
        
        var tokenJWT = new JwtSecurityToken(
            expires: expiration,
            signingCredentials: credentials,
            claims: claims
        );

        return new UserToken()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(tokenJWT),
            Expiration = expiration
        };
    }
}

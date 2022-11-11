using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserLogic UserLogic;
    private readonly IConfiguration config;

    public UsersController(IUserLogic userLogic, IConfiguration config)
    {
        UserLogic = userLogic;
        this.config = config;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserCreationDTO>> CreateUserAsync(User user)
    {
        try
        {
            UserCreationDTO userCreation = await UserLogic.CreateUser(user);
            String token = GenerateJwt(userCreation);

            return Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserCreationDTO>> LoginAsync(UserLoginDTO userLoginDto)
    {
        try
        {
            UserCreationDTO userCreationDto = await UserLogic.LogIn(userLoginDto.userName, userLoginDto.password);
            String token = GenerateJwt(userCreationDto);

            return Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest(e.Message);
        }
    }

    private List<Claim> GenerateClaims(UserCreationDTO user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("Username", user.userName)
        };
        return claims.ToList();
    }
    
    private string GenerateJwt(UserCreationDTO user)
    {
        List<Claim> claims = GenerateClaims(user);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            config["Jwt:Issuer"],
            config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
}
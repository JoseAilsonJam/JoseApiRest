using JoseApiRest.Domain.Entitys;
using JoseApiRest.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoseApiRest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService) => _jwtService = jwtService;

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (request.Username == "admin" && request.Password == "password")
        {
            var token = _jwtService.GenerateToken(request.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized("Credenciais inválidas.");
    }
}
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly Dictionary<string, string> _users = new Dictionary<string, string>
    {
        { "username", "password" } // Replace with your actual user data
    };

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (_users.TryGetValue(request.Username, out var expectedPassword) && expectedPassword == request.Password)
        {
            var token = _jwtService.GenerateToken(request.Username);
            return Ok(new { token });
        }
        return Unauthorized();
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Services;

namespace ShoppingCartAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 🔹 Validar usuario (esto debería ir a una base de datos real)
            if (request.Username == "admin" && request.Password == "admin123")
            {
                var token = _jwtService.GenerateToken("1", "Admin");
                return Ok(new { Token = token });
            }

            return Unauthorized(new { Message = "Credenciales inválidas" });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

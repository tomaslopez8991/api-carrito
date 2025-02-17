using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Services;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Autentica al usuario y devuelve un token JWT. (usuario=admin , password=admin123)
        /// </summary>
        /// <param name="loginRequest">Credenciales del usuario.</param>
        /// <returns>Token de autenticación.</returns>
        /// <response code="200">Autenticación exitosa, devuelve un token JWT.</response>
        /// <response code="400">Solicitud incorrecta, faltan credenciales.</response>
        /// <response code="401">Credenciales inválidas.</response>
        [SwaggerOperation(Summary = "Autenticación de usuario", Description = "Recibe un usuario y contraseña, devuelve un token JWT si las credenciales son correctas.")]
        [SwaggerResponse(200, "Autenticación exitosa", typeof(string))]
        [SwaggerResponse(400, "Solicitud incorrecta, faltan credenciales.")]
        [SwaggerResponse(401, "Credenciales inválidas.")]
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

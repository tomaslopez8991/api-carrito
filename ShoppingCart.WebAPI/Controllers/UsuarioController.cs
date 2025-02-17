using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application.Services;
using ShoppingCart.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Controllers
{
    [Authorize]
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Crea un usuario para operar dentro de la API.
        /// </summary>
        /// <returns>Retorna el ID del usuario creado.</returns>
        /// <response code="200">Usuario creado exitosamente.</response>
        /// <response code="400">Datos inválidos.</response>
        [HttpPost("crear-usuario")]
        public async Task<IActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Dni))
                return BadRequest("El DNI es obligatorio.");

            var usuarioCreado = await _usuarioService.CrearUsuarioAsync(usuario);
            return CreatedAtAction(nameof(CrearUsuario), new { dni = usuarioCreado.Dni }, usuarioCreado);
        }
    }
}

using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Exceptions;
using ShoppingCart.Domain.Interfaces;
using System.Threading.Tasks;

namespace ShoppingCart.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> CrearUsuarioAsync(Usuario usuarioDto)
        {
            if (string.IsNullOrEmpty(usuarioDto.Dni))
                throw new ValidationException("El DNI del usuario es obligatorio.");

            var usuarioExistente = await _usuarioRepository.ObtenerUsuarioPorDNIAsync(usuarioDto.Dni);
            if (usuarioExistente != null)
                throw new ValidationException("Ya existe un usuario con este DNI.");

            var usuario = new Usuario
            {
                Dni = usuarioDto.Dni,
                EsVip = usuarioDto.EsVip
            };

            var usuarioCreado = await _usuarioRepository.CrearUsuarioAsync(usuario);
            return new Usuario
            {
                Dni = usuarioCreado.Dni,
                EsVip = usuarioCreado.EsVip
            };
        }
    }
}

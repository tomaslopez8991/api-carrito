using Microsoft.EntityFrameworkCore;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Infrastructure.Persistence;
using System.Threading.Tasks;

namespace ShoppingCart.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ShoppingCartDbContext _context;

        public UsuarioRepository(ShoppingCartDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ObtenerUsuarioPorDNIAsync(string dni)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Dni == dni);
        }

        public async Task<Usuario> CrearUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}

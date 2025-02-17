using ShoppingCart.Domain.Entities;
using System.Threading.Tasks;

namespace ShoppingCart.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObtenerUsuarioPorDNIAsync(string dni);
        Task<Usuario> CrearUsuarioAsync(Usuario usuario);
    }
}

using ShoppingCart.Domain.Entities;

namespace ShoppingCart.Domain.Interfaces
{
    public interface ICarritoRepository
    {
        Task<Carrito> CrearCarritoAsync(Carrito carrito);
        Task<Carrito?> ObtenerCarritoPorIdAsync(int carritoId);
        Task EliminarCarritoAsync(int carritoId);
        Task<Carrito> AgregarProductoAsync(int carritoId, int productoId);
        Task<Carrito> EliminarProductoAsync(int carritoId, int productoId);
        Task<decimal> CalcularTotalAsync(int carritoId);
        Task<List<Producto>> ObtenerProductosMasCarosAsync(string dniUsuario);
    }
}
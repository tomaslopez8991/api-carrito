using Microsoft.EntityFrameworkCore;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Infrastructure.Persistence;
using ShoppingCart.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using ShoppingCart.Domain.Enums;
using System.Net;

public class CarritoRepository : ICarritoRepository
{
    private readonly ShoppingCartDbContext _context;
    private readonly ILogger<CarritoRepository> _logger;

    public CarritoRepository(ShoppingCartDbContext context, ILogger<CarritoRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Carrito> CrearCarritoAsync(Carrito carrito)
    {
        try
        {
            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();
            return carrito;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear un carrito para el usuario {UsuarioDNI}", carrito.UsuarioDNI);
            throw;
        }
    }

    public async Task<Carrito?> ObtenerCarritoPorIdAsync(int carritoId)
    {
        try
        {
            return await _context.Carritos
                .Include(c => c.Items)
                .ThenInclude(i => i.Producto)
                .FirstOrDefaultAsync(c => c.Id == carritoId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener el carrito con ID {CarritoId}", carritoId);
            throw;
        }
    }

    public async Task EliminarCarritoAsync(int carritoId)
    {
        var carrito = await ObtenerCarritoPorIdAsync(carritoId);
        if (carrito == null)
            throw new NotFoundException($"No se encontró el carrito con ID {carritoId}.");

        _context.Carritos.Remove(carrito);
        await _context.SaveChangesAsync();
    }

    public async Task<Carrito> AgregarProductoAsync(int carritoId, int productoId)
    {
        try
        {
            var carrito = await ObtenerCarritoPorIdAsync(carritoId);
            var producto = await _context.Productos.FindAsync(productoId);

            if (carrito == null)
                throw new NotFoundException($"No se encontró el carrito con ID {carritoId}.");
            if (producto == null)
                throw new NotFoundException($"No se encontró el producto con ID {productoId}.");

            carrito.Items.Add(new ItemCarrito { ProductoId = productoId });
            await _context.SaveChangesAsync();
            return carrito;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al agregar el producto {ProductoId} al carrito {CarritoId}", productoId, carritoId);
            throw;
        }
    }

    public async Task<decimal> CalcularTotalAsync(int carritoId)
    {
        var carrito = await _context.Carritos
        .Include(c => c.Items)
        .ThenInclude(i => i.Producto)
        .FirstOrDefaultAsync(c => c.Id == carritoId);

        if (carrito == null || carrito.Items.Count == 0)
            return 0;

        decimal total = carrito.Items.Sum(i => i.Producto.Precio);
        int cantidadProductos = carrito.Items.Count;

        if (cantidadProductos == 5)
        {
            total *= 0.8m;
        }
        else if (cantidadProductos > 10)
        {
            switch (carrito.TipoCarrito)
            {
                case TipoCarrito.Comun:
                    total -= 200;
                    break;
                case TipoCarrito.PromoFechaEspecial:
                    total -= 500;
                    break;
                case TipoCarrito.PromoUsuarioVIP:
                    var productoMasBarato = carrito.Items.Min(i => i.Producto.Precio);
                    total -= productoMasBarato;
                    total -= 700;
                    break;
            }
        }

        return total > 0 ? total : 0;
    }

    public async Task<List<Producto>> ObtenerProductosMasCarosAsync(string dniUsuario)
    {
        return await _context.ItemsCarrito
            .Where(i => i.Carrito.UsuarioDNI == dniUsuario)
            .OrderByDescending(i => i.Producto.Precio)
            .Select(i => i.Producto)
            .Distinct()
            .Take(4)
            .ToListAsync();
    }

    public async Task<Carrito> EliminarProductoAsync(int carritoId, int productoId)
    {
        try
        {
            var carrito = await ObtenerCarritoPorIdAsync(carritoId);
            if (carrito == null)
                throw new NotFoundException($"No se encontró el carrito con ID {carritoId}.");

            var item = carrito.Items.FirstOrDefault(i => i.ProductoId == productoId);
            if (item == null)
                throw new NotFoundException($"El producto con ID {productoId} no está en el carrito.");

            carrito.Items.Remove(item);
            await _context.SaveChangesAsync();
            return carrito;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar el producto {ProductoId} del carrito {CarritoId}", productoId, carritoId);
            throw;
        }
    }
}

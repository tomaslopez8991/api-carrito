using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Exceptions;
using ShoppingCart.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ShoppingCart.Domain.Enums;

namespace ShoppingCart.Application.Services;

public class CarritoService
{
    private readonly ICarritoRepository _carritoRepository;
    private readonly IDistributedCache _cache;

    public CarritoService(ICarritoRepository carritoRepository, IDistributedCache cache)
    {
        _carritoRepository = carritoRepository;
        _cache = cache;
    }

    public async Task<Carrito> CrearCarritoAsync(string dniUsuario, bool esFechaPromocional)
    {
        if (string.IsNullOrEmpty(dniUsuario))
            throw new ValidationException("El DNI del usuario es requerido.");

        var usuario = await _carritoRepository.ObtenerUsuarioPorDNIAsync(dniUsuario);
        if (usuario == null)
            throw new NotFoundException($"No se encontró un usuario con el DNI {dniUsuario}.");

        TipoCarrito tipoCarrito;
        if (usuario.EsVip)
        {
            tipoCarrito = TipoCarrito.PromoUsuarioVIP;
        }
        else if (esFechaPromocional)
        {
            tipoCarrito = TipoCarrito.PromoFechaEspecial;
        }
        else
        {
            tipoCarrito = TipoCarrito.Comun;
        }

        var carrito = new Carrito
        {
            UsuarioDNI = dniUsuario,
            TipoCarrito = tipoCarrito
        };

        return await _carritoRepository.CrearCarritoAsync(carrito);
    }

    public async Task EliminarCarritoAsync(int carritoId)
    {
        var carrito = await _carritoRepository.ObtenerCarritoPorIdAsync(carritoId);
        if (carrito == null)
            throw new NotFoundException($"No se encontró el carrito con ID {carritoId}.");

        await _carritoRepository.EliminarCarritoAsync(carritoId);
    }

    public async Task<Carrito> AgregarProductoAsync(int carritoId, int productoId)
    {
        var carrito = await _carritoRepository.ObtenerCarritoPorIdAsync(carritoId);
        if (carrito == null)
            throw new NotFoundException($"No se encontró el carrito con ID {carritoId}.");

        return await _carritoRepository.AgregarProductoAsync(carritoId, productoId);
    }

    public async Task<Carrito> EliminarProductoAsync(int carritoId, int productoId)
    {
        var carrito = await _carritoRepository.ObtenerCarritoPorIdAsync(carritoId);
        if (carrito == null)
            throw new NotFoundException($"No se encontró el carrito con ID {carritoId}.");

        return await _carritoRepository.EliminarProductoAsync(carritoId, productoId);
    }

    public async Task<decimal> CalcularTotalAsync(int carritoId)
    {
        return await _carritoRepository.CalcularTotalAsync(carritoId);
    }

    public async Task<List<Producto>> ObtenerProductosMasCarosAsync(string dniUsuario)
    {
        return await _carritoRepository.ObtenerProductosMasCarosAsync(dniUsuario);
    }

    public async Task<Usuario> ObtenerUsuarioPorDNIAsync(string dni)
    {
        var usuario = await _carritoRepository.ObtenerUsuarioPorDNIAsync(dni);
        return usuario != null ? new Usuario
        {
            Dni = usuario.Dni,
            EsVip = usuario.EsVip
        } : null;
    }
}

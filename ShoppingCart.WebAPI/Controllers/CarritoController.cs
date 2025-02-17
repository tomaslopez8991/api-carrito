using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Application.Services;
using ShoppingCart.Domain.Entities;
using ShoppingCart.Domain.Interfaces;

namespace ShoppingCartAPI.Controllers;

/// <summary>
/// Controlador para la gestión del carrito de compras.
/// </summary>
[Authorize]
[ApiController]
[Route("api/carrito")]
public class CarritoController : ControllerBase
{
    private readonly CarritoService _carritoService;

    public CarritoController(CarritoService carritoService)
    {
        _carritoService = carritoService;
    }

    /// <summary>
    /// Crea un nuevo carrito para un usuario.
    /// </summary>
    /// <param name="dniUsuario">DNI del usuario.</param>
    /// <param name="esFechaPromocional">Indica si la fecha es promocional.</param>
    /// <returns>Retorna el ID del carrito creado.</returns>
    /// <response code="200">Carrito creado exitosamente.</response>
    /// <response code="400">Datos inválidos.</response>
    [HttpPost("crear")]
    public async Task<IActionResult> CrearCarrito([FromQuery] string dniUsuario, [FromQuery] bool esFechaPromocional)
    {
        var carrito = await _carritoService.CrearCarritoAsync(dniUsuario, esFechaPromocional);
        return Ok(new { CarritoId = carrito.Id });
    }

    /// <summary>
    /// Elimina un carrito existente.
    /// </summary>
    /// <param name="carritoId">ID del carrito a eliminar.</param>
    /// <returns>Retorna 204 si se eliminó correctamente.</returns>
    [HttpDelete("{carritoId}")]
    public async Task<IActionResult> EliminarCarrito(int carritoId)
    {
        await _carritoService.EliminarCarritoAsync(carritoId);
        return NoContent();
    }

    /// <summary>
    /// Agrega un producto a un carrito.
    /// </summary>
    /// <param name="carritoId">ID del carrito.</param>
    /// <param name="productoId">ID del producto.</param>
    /// <returns>Retorna el estado actualizado del carrito.</returns>
    [HttpPost("{carritoId}/agregar-producto/{productoId}")]
    public async Task<IActionResult> AgregarProducto(int carritoId, int productoId)
    {
        var carrito = await _carritoService.AgregarProductoAsync(carritoId, productoId);
        return Ok(carrito);
    }

    /// <summary>
    /// Elimina un producto de un carrito
    /// </summary>
    [HttpDelete("{carritoId}/eliminar-producto/{productoId}")]
    public async Task<IActionResult> EliminarProducto(int carritoId, int productoId)
    {
        var carrito = await _carritoService.EliminarProductoAsync(carritoId, productoId);
        return Ok(carrito);
    }

    /// <summary>
    /// Obtiene el total a pagar de un carrito.
    /// </summary>
    /// <param name="carritoId">ID del carrito.</param>
    /// <returns>El total a pagar.</returns>
    [HttpGet("{carritoId}/total")]
    public async Task<IActionResult> ObtenerTotalCarrito(int carritoId)
    {
        var total = await _carritoService.CalcularTotalAsync(carritoId);
        return Ok(new { Total = total });
    }

    /// <summary>
    /// Devuelve los 4 productos más caros comprados por un usuario.
    /// </summary>
    [HttpGet("productos-mas-caros/{dniUsuario}")]
    public async Task<IActionResult> ObtenerProductosMasCaros(string dniUsuario)
    {
        var productos = await _carritoService.ObtenerProductosMasCarosAsync(dniUsuario);
        if (productos == null || productos.Count == 0)
            return NotFound("No se encontraron compras para este usuario.");

        return Ok(productos);
    }

}

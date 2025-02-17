using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShoppingCart.Domain.Entities;

public class ItemCarrito
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Carrito")]
    public int CarritoId { get; set; }

    [Required]
    [ForeignKey("Producto")]
    public int ProductoId { get; set; }

    [JsonIgnore]
    public Carrito Carrito { get; set; }
    public Producto Producto { get; set; }
}

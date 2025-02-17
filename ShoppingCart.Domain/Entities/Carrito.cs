using ShoppingCart.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShoppingCart.Domain.Entities;

public class Carrito
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    public string UsuarioDNI { get; set; }

    [Required]
    public TipoCarrito TipoCarrito { get; set; }

    [JsonIgnore]
    public Usuario Usuario { get; set; }

    public List<ItemCarrito> Items { get; set; } = new();
}

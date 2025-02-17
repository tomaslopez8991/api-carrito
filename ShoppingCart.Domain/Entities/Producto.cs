using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Domain.Entities;

public class Producto
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Precio { get; set; }
}

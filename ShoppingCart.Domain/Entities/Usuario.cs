using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Domain.Entities;

public class Usuario
{
    [Key]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "El DNI debe tener 8 caracteres.")]
    public string Dni { get; set; }

    public bool EsVip { get; set; }
}

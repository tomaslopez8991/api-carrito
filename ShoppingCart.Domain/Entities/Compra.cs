namespace ShoppingCart.Domain.Entities;

public class Compra
{
    public int Id { get; set; }
    public string UsuarioDNI { get; set; }
    public int ProductoId { get; set; }
    public DateTime FechaCompra { get; set; }
    public Producto Producto { get; set; }
    public Usuario Usuario { get; set; }
}

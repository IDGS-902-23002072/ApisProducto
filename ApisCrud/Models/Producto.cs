namespace ApisCrud.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public double PrecioUnitario { get; set; }
        public bool Estatus { get; set; }
        public int Existencias { get; set; }

        public Tipos Tipo { get; set; }

        public enum Tipos
        {
            Articulo,
            Servicio
        }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
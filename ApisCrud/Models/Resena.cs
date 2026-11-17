namespace ApisCrud.Models
{
    public class Resena
    {
        public int IdResena { get; set; }
        public int IdProducto { get; set; }
        public int IdUsuario { get; set; }
        public int Calificacion { get; set; }
        public string? Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estatus { get; set; } = true;
    }
}
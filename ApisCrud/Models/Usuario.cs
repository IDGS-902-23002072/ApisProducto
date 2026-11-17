namespace ApisCrud.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string? Nombre { get; set; }

        public string? Correo { get; set; }

        public string? Telefono { get; set; }

        public string? Contrasena { get; set; }

        public bool Estatus { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
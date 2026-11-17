using ApisCrud.Data;
using ApisCrud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApisCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly InventarioContext _context;
        private readonly PasswordHasher<Usuario> _hasher = new();

        public UsuarioController(InventarioContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(u => new UsuarioDTO
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    Correo = u.Correo,
                    Telefono = u.Telefono,
                    Estatus = u.Estatus,
                    CreatedAt = u.CreatedAt
                })
                .ToListAsync();

            return usuarios;
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Telefono = usuario.Telefono,
                Estatus = usuario.Estatus,
                CreatedAt = usuario.CreatedAt
            };
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario(Usuario usuario)
        {
            // DIAGNÓSTICO: devuelve el error real en lugar de un 500 vacío.
            // Quitar el try/catch cuando se encuentre la causa.
            try
            {
                if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                    return BadRequest("La contraseña es obligatoria.");

                bool correoExiste = await _context.Usuarios
                    .AnyAsync(u => u.Correo == usuario.Correo);

                if (correoExiste)
                    return BadRequest("El correo ya está registrado.");

                usuario.CreatedAt = DateTime.UtcNow;
                usuario.Contrasena = _hasher.HashPassword(
                    usuario,
                    usuario.Contrasena
                );

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetUsuario),
                    new { id = usuario.IdUsuario },
                    new UsuarioDTO
                    {
                        IdUsuario = usuario.IdUsuario,
                        Nombre = usuario.Nombre,
                        Correo = usuario.Correo,
                        Telefono = usuario.Telefono,
                        Estatus = usuario.Estatus,
                        CreatedAt = usuario.CreatedAt
                    }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return StatusCode(500, ex.InnerException?.Message ?? ex.Message);
            }
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
                return BadRequest("El ID no coincide.");

            var existente = await _context.Usuarios.FindAsync(id);

            if (existente == null)
                return NotFound("Usuario no encontrado.");

            bool correoExiste = await _context.Usuarios
                .AnyAsync(u => u.Correo == usuario.Correo &&
                               u.IdUsuario != id);

            if (correoExiste)
                return BadRequest("El correo ya pertenece a otro usuario.");

            existente.Nombre = usuario.Nombre;
            existente.Correo = usuario.Correo;
            existente.Telefono = usuario.Telefono;
            existente.Estatus = usuario.Estatus;

            if (!string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                existente.Contrasena = _hasher.HashPassword(
                    existente,
                    usuario.Contrasena
                );
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
                return NotFound();

            usuario.Estatus = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class UsuarioDTO
        {
            public int IdUsuario { get; set; }
            public string? Nombre { get; set; }
            public string? Correo { get; set; }
            public string? Telefono { get; set; }
            public bool Estatus { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }
}

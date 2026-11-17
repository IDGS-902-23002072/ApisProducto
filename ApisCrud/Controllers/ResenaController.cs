
using ApisCrud.Data;
using ApisCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApisCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResenaController : ControllerBase
    {
        private readonly InventarioContext _context;

        public ResenaController(InventarioContext context)
        {
            _context = context;
        }

        // GET: api/Resenas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resena>>> GetResenas()
        {
            return await _context.Resenas.ToListAsync();
        }

        // GET: api/Resenas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resena>> GetResena(int id)
        {
            var resena = await _context.Resenas.FindAsync(id);

            if (resena == null)
                return NotFound("Reseña no encontrada.");

            return resena;
        }

        // POST: api/Resenas
        [HttpPost]
        public async Task<ActionResult<Resena>> PostResena(Resena resena)
        {
            if (resena.Calificacion < 1 || resena.Calificacion > 5)
                return BadRequest("La calificación debe estar entre 1 y 5.");

            var producto = await _context.Productos
                .FindAsync(resena.IdProducto);

            if (producto == null)
                return BadRequest("El producto no existe.");

            var usuario = await _context.Usuarios
                .FindAsync(resena.IdUsuario);

            if (usuario == null)
                return BadRequest("El usuario no existe.");

            resena.Fecha = DateTime.UtcNow;

            _context.Resenas.Add(resena);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetResena),
                new { id = resena.IdResena },
                resena
            );
        }

        // PUT: api/Resenas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResena(int id, Resena resena)
        {
            if (id != resena.IdResena)
                return BadRequest("El ID no coincide.");

            if (resena.Calificacion < 1 || resena.Calificacion > 5)
                return BadRequest("La calificación debe estar entre 1 y 5.");

            var existente = await _context.Resenas.FindAsync(id);

            if (existente == null)
                return NotFound("Reseña no encontrada.");

            bool productoExiste = await _context.Productos
                .AnyAsync(p => p.IdProducto == resena.IdProducto);

            if (!productoExiste)
                return BadRequest("El producto no existe.");

            bool usuarioExiste = await _context.Usuarios
                .AnyAsync(u => u.IdUsuario == resena.IdUsuario);

            if (!usuarioExiste)
                return BadRequest("El usuario no existe.");

            existente.IdProducto = resena.IdProducto;
            existente.IdUsuario = resena.IdUsuario;
            existente.Calificacion = resena.Calificacion;
            existente.Comentario = resena.Comentario;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResena(int id)
        {
            var resena = await _context.Resenas.FindAsync(id);

            if (resena == null)
                return NotFound();

            resena.Estatus = false;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

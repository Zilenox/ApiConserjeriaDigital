using ApiConserjeriaDigital.Database;
using ApiConserjeriaDigital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiConserjeriaDigital.Controllers
{
    //Pizarra
    [Route("api/[controller]")]
    [ApiController]

    public class PublicacionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PublicacionController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<PublicacionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publicacion>>> GetPublicacion()
        {
            return await _context.Pizarra.ToListAsync();
        }

        // GET api/<PublicacionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publicacion>> GetPublicacion(int id)
        {
            var publicacion = await _context.Pizarra.FindAsync(id);

            if (publicacion == null)
            {
                return NotFound();
            }
            return publicacion;
        }

        // POST api/<PublicacionController>
        [HttpPost]
        public async Task<ActionResult<Publicacion>> PostPublicacion([FromBody] Publicacion publicacion)
        {
            _context.Pizarra.Add(publicacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPublicacion", new { id = publicacion.Id }, publicacion);
        }

        // PUT api/<PublicacionController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublicacion(int id, Publicacion publicacion)
        {
            if (id != publicacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(publicacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<PublicacionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublicacion(int id)
        {
            var publicacion = await _context.Pizarra.FindAsync(id);
            if (publicacion == null)
            {
                return NotFound();
            }

            _context.Pizarra.Remove(publicacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublicacionExists(int id)
        {
            return _context.Pizarra.Any(e => e.Id == id);
        }
    }
}

using ApiConserjeriaDigital.Database;
using ApiConserjeriaDigital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiConserjeriaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConserjeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConserjeController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conserje>>> GetConserjes()
        {
            return await _context._conserje.ToListAsync();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conserje>> GetConserje(int id)
        {
            var usuario = await _context._conserje.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<Conserje>> PostUsuario([FromBody] Conserje conserje)
        {
            _context._conserje.Add(conserje);
            trash auth = new trash();
            auth.RUT = conserje.RUT;
            auth.pass = "Conserje";
            _context.temp.Add(auth);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConserje", new { id = conserje.ID }, conserje);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConserje(int id, Conserje conserje)
        {
            if (id != conserje.ID)
            {
                return BadRequest();
            }

            _context.Entry(conserje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConserjeExists(id))
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


        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConserje(int id)
        {
            var usuario = await _context._conserje.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context._conserje.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConserjeExists(int id)
        {
            return _context._conserje.Any(e => e.ID == id);
        }
    }
}

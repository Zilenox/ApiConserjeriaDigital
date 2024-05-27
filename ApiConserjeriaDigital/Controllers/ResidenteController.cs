using ApiConserjeriaDigital.Database;
using ApiConserjeriaDigital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiConserjeriaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResidenteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ResidenteController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<ResidenteController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Residente>>> GetResidente()
        {
            return await _context._residente.ToListAsync();
        }

        // GET api/<ResidenteController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Residente>> GetResidente(int id)
        {

            IEnumerable<Residente> residenteLookup = await _context._residente.ToListAsync();

            foreach (Residente res in residenteLookup)
            {
                if (res.RUT == id)
                {
                    return res;
                }
            }
            return NotFound();

        }

        // POST api/<ResidenteController>
        [HttpPost]
        public async Task<ActionResult<User>> PostResidente([FromBody] Residente res)
        {
            _context._residente.Add(res);
            trash auth = new trash();
            auth.RUT = res.RUT;
            auth.pass = res.NumeroDepto.ToString();
            _context.temp.Add(auth);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetResidente", new { id = res.ID }, res);
        }

        // PUT api/<ResidenteController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResidente(int id, Residente residente)
        {
            if (id != residente.ID)
            {
                return BadRequest();
            }

            _context.Entry(residente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidenteExists(id))
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

        // DELETE api/<ResidenteController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResidente(int id)
        {
            var residente = await _context._residente.FindAsync(id);
            if (residente == null)
            {
                return NotFound();
            }

            _context._residente.Remove(residente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResidenteExists(int id)
        {
            return _context._residente.Any(e => e.ID == id);
        }
    }
}

using ApiConserjeriaDigital.Database;
using ApiConserjeriaDigital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiConserjeriaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorrespondenciaController : ControllerBase
    {
        //Casilla
        private readonly ApplicationDbContext _context;
        public CorrespondenciaController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/<CorrespondenciaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Correspondencia>>> GetCorrespondencia()
        {
            return await _context.Casilla.ToListAsync();
        }

        // GET api/<CorrespondenciaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Correspondencia>> GetCorrespondencia(int id)
        {
            var correo = await _context.Casilla.FindAsync(id);

            if (correo == null)
            {
                return NotFound();
            }
            return correo;
        }

        // POST api/<CorrespondenciaController>
        [HttpPost]
        public async Task<ActionResult<Correspondencia>> PostCorrespondencia([FromBody] Correspondencia correo)
        {
            IEnumerable<Residente> residenteLookup = await _context._residente.ToListAsync();
            foreach (Residente res in residenteLookup)
            {
                if (res.NumeroDepto == correo.Destinatario)
                {
                    _context.Casilla.Add(correo);
                    await _context.SaveChangesAsync();

                    bool test = await modifCasillaAsync(res, 1);
                    if (!test)
                        return NotFound();
                    return CreatedAtAction("GetCorrespondencia", new { id = correo.Id }, correo);
                }
            }
            return NotFound();
        }

        // PUT api/<CorrespondenciaController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCorrespondencia(int id, Correspondencia correo)
        {
            if (id != correo.Id)
            {
                return BadRequest();
            }

            _context.Entry(correo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorrespondenciaExists(id))
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

        // DELETE api/<CorrespondenciaController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCorrespondencia(int id)
        {
            var Correo = await _context.Casilla.FindAsync(id);
            if (Correo == null)
            {
                return NotFound();
            }

            IEnumerable<Residente> residenteLookup = await _context._residente.ToListAsync();
            foreach (Residente res in residenteLookup)
            {
                if (res.NumeroDepto == Correo.Destinatario)
                {
                    await modifCasillaAsync(res, -1);
                }
            }
            _context.Casilla.Remove(Correo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CorrespondenciaExists(int id)
        {
            return _context.Casilla.Any(e => e.Id == id);
        }

        private async Task<bool> modifCasillaAsync(Residente residente, int value)
        {
            Residente? tempResidente = residente;
            if (tempResidente == null)
            {
                return false;
            }

            tempResidente.Casilla += value;
            _context.Entry(tempResidente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidenteExists(residente.ID))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        private bool ResidenteExists(int id)
        {
            return _context._residente.Any(e => e.ID == id);
        }
    }
}

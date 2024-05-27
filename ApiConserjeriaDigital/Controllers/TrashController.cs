using ApiConserjeriaDigital.Database;
using ApiConserjeriaDigital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiConserjeriaDigital.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrashController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TrashController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<TrashController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<trash>>> GetResidente()
        {
            return await _context.temp.ToListAsync();
        }

        // POST api/<TrashController>
        [HttpPost]
        public async Task<ActionResult<trash>> LoginUsuario([FromBody] trash credentials)
        {
            var keyholder = await _context.temp.ToListAsync();

            foreach(var key in keyholder)
            {
                if (key.pass == credentials.pass && key.RUT == credentials.RUT)
                    return Ok();
            }
            return BadRequest();
        }


        [HttpPut("{_RUT}")]
        public async Task<IActionResult> PutCredentials(int _RUT, trash credentials)
        {
            if (_RUT != credentials.RUT)
            {
                return BadRequest();
            }

            _context.Entry(credentials).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthExist(_RUT))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        private bool AuthExist(int _RUT)
        {
            return _context.temp.Any(e => e.RUT == _RUT);
        }

    }
}

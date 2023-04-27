using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Repository;

namespace personapi_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelefonosAPIController : Controller
    {
        private readonly SumaDbContext _context;

        public TelefonosAPIController(SumaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTelefonos()
        {
            return _context.Telefonos != null ?
                          Ok(await _context.Telefonos.ToListAsync()) :
                          Problem("Entity set 'PersonaDbContext.Telefonos'  is null.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTelefono(string id)
        {
            if (id == null || _context.Telefonos == null)
            {
                return NotFound();
            }

            var telefono = await _context.Telefonos
                .FirstOrDefaultAsync(m => m.Num == id);
            if (telefono == null)
            {
                return NotFound();
            }

            return Ok(telefono);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutTelefono(string id, Telefono telefono)
        {
            if (id != telefono.Num)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(telefono);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelefonoExists(telefono.Num))
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
            return BadRequest(ModelState);
        }

    
        [HttpPost]
        public async Task<ActionResult<Telefono>> PostTelefono(Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                _context.Add(telefono);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTelefono), new { id = telefono.Num }, telefono);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTelefono(string id)
        {
            if (_context.Telefonos == null)
            {
                return Problem("Entity set 'PersonaDbContext.Telefonos'  is null.");
            }
            var telefono = await _context.Telefonos.FindAsync(id);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }

        private bool TelefonoExists(string id)
        {
            return (_context.Telefonos?.Any(e => e.Num == id)).GetValueOrDefault();
        }
    }
}

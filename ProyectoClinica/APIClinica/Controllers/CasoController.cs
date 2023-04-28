using ClinicaModels;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using APIClinica.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("Case")]
    public class CasoController : Controller
    {
        [HttpGet("GetCasesList")]
        public async Task<ActionResult<IEnumerable<Models.Caso>>> GetList()
        {

            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Casos == null)
            {
                return NotFound();
            }
            return await _ClinicaContext.Casos.ToListAsync();
        }

        [HttpGet("GetCase/{id}")]
        public async Task<ActionResult<Models.Caso>> Get(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Casos == null)
            {
                return NotFound();
            }
            var caso = await _ClinicaContext.Casos.FindAsync(id);

            if (caso == null)
            {
                return NotFound();
            }
            return caso;
        }


        [HttpPut("EditCase/{id}")]
        public async Task<IActionResult> Put(int id, Models.Caso caso)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (id != caso.Id)
            {
                return BadRequest();
            }

            _ClinicaContext.Entry(caso).State = EntityState.Modified;

            try
            {
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(id))
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

        [HttpPost("CreateCase")]
        public async Task<ActionResult<Models.Caso>> Post(Models.Caso caso)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Casos == null)
            {
                return Problem("Entity set 'ClinicaContext.Casos' is null.");
            }
            _ClinicaContext.Casos.Add(caso);
            await _ClinicaContext.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = caso.Id }, caso);
        }

        [HttpDelete("DeleteCase/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Casos == null)
            {
                return NotFound();
            }
            var caso = await _ClinicaContext.Casos.FindAsync(id);
            if (caso == null)
            {
                return NotFound();
            }

            _ClinicaContext.Casos.Remove(caso);
            await _ClinicaContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CaseExists(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            return (_ClinicaContext.Casos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

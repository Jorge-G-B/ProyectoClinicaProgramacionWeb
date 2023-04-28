using ClinicaModels;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using APIClinica.Models;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("Consulta")]
    public class ConsultaController : Controller
    {
        [HttpGet("GetConsultasList")]
        public async Task<ActionResult<IEnumerable<Models.Consultum>>> GetList()
        {

            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Consulta == null)
            {
                return NotFound();
            }
            return await _ClinicaContext.Consulta.ToListAsync();
        }

        [HttpGet("GetConsulta/{id}")]
        public async Task<ActionResult<Models.Consultum>> Get(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Consulta == null)
            {
                return NotFound();
            }
            var consulta = await _ClinicaContext.Consulta.FindAsync(id);

            if (consulta == null)
            {
                return NotFound();
            }
            return consulta;
        }


        [HttpPut("EditConsulta/{id}")]
        public async Task<IActionResult> Put(int id, Models.Consultum consulta)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (id != consulta.Id)
            {
                return BadRequest();
            }

            _ClinicaContext.Entry(consulta).State = EntityState.Modified;

            try
            {
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultaExists(id))
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

        [HttpPost("CreateConsulta")]
        public async Task<ActionResult<Models.Consultum>> Post(Models.Consultum consulta)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Consulta == null)
            {
                return Problem("Entity set 'ClinicaContext.Consulta' is null.");
            }
            _ClinicaContext.Consulta.Add(consulta);
            await _ClinicaContext.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = consulta.Id }, consulta);
        }

        [HttpDelete("DeleteConsulta/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Consulta == null)
            {
                return NotFound();
            }
            var consulta = await _ClinicaContext.Consulta.FindAsync(id);
            if (consulta == null)
            {
                return NotFound();
            }

            _ClinicaContext.Consulta.Remove(consulta);
            await _ClinicaContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ConsultaExists(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            return (_ClinicaContext.Consulta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

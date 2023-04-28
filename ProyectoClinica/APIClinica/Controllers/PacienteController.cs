using ClinicaModels;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using APIClinica.Models;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("Pacient")]
    public class PacienteController : Controller
    {
        [HttpGet("GetPacientsList")]
        public async Task<ActionResult<IEnumerable<Models.Paciente>>> GetList()
        {

            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Pacientes == null)
            {
                return NotFound();
            }
            return await _ClinicaContext.Pacientes.ToListAsync();
        }

        [HttpGet("GetPacient/{id}")]
        public async Task<ActionResult<Models.Paciente>> Get(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Pacientes == null)
            {
                return NotFound();
            }
            var pacient = await _ClinicaContext.Pacientes.FindAsync(id);

            if (pacient == null)
            {
                return NotFound();
            }
            return pacient;
        }


        [HttpPut("EditPacient/{id}")]
        public async Task<IActionResult> Put(int id, Models.Paciente pacient)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (id != pacient.Id)
            {
                return BadRequest();
            }

            _ClinicaContext.Entry(pacient).State = EntityState.Modified;

            try
            {
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacientExists(id))
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

        [HttpPost("CreatePacient")]
        public async Task<ActionResult<Models.Paciente>> Post(Models.Paciente pacient)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Pacientes == null)
            {
                return Problem("Entity set 'ClinicaContext.Pacientes' is null.");
            }
            _ClinicaContext.Pacientes.Add(pacient);
            await _ClinicaContext.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = pacient.Id }, pacient);
        }

        [HttpDelete("DeletePacient/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Pacientes == null)
            {
                return NotFound();
            }
            var pacient = await _ClinicaContext.Pacientes.FindAsync(id);
            if (pacient == null)
            {
                return NotFound();
            }

            _ClinicaContext.Pacientes.Remove(pacient);
            await _ClinicaContext.SaveChangesAsync();

            return NoContent();
        }

        private bool PacientExists(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            return (_ClinicaContext.Pacientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

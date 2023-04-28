using APIClinica.Models;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using ClinicaModels;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("Role")]
    public class RoleController : Controller
    {
        [HttpGet("GetRolesList")]
        public async Task<ActionResult<IEnumerable<Role>>> GetList()
        {

            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Roles == null)
            {
                return NotFound();
            }
            return await _ClinicaContext.Roles.ToListAsync();
        }

        [HttpGet("GetRole/{id}")]
        public async Task<ActionResult<Role>> Get(short id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Roles == null)
            {
                return NotFound();
            }
            var rol = await _ClinicaContext.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }


        [HttpPut("EditRole/{id}")]
        public async Task<IActionResult> Put(short id, Role rol)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (id != rol.Id)
            {
                return BadRequest();
            }

            _ClinicaContext.Entry(rol).State = EntityState.Modified;

            try
            {
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolExists(id))
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

        [HttpPost("CreateRole")]
        public async Task<ActionResult<Role>> Post(Role rol)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Roles == null)
            {
                return Problem("Entity set 'ClinicaContext.Roles' is null.");
            }
            _ClinicaContext.Roles.Add(rol);
            await _ClinicaContext.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = rol.Id }, rol);
        }

        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> Delete(short id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Roles == null)
            {
                return NotFound();
            }
            var rol = await _ClinicaContext.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            _ClinicaContext.Roles.Remove(rol);
            await _ClinicaContext.SaveChangesAsync();

            return NoContent();
        }

        private bool RolExists(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            return (_ClinicaContext.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

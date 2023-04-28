using ClinicaModels;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using APIClinica.Models;
using Microsoft.EntityFrameworkCore;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        [Route("GetUsersList")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetList()
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Users == null)
            {
                return NotFound();
            }
            return await _ClinicaContext.Users.ToListAsync();
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Users == null)
            {
                return NotFound();
            }
            var user = await _ClinicaContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        [HttpPut("EditUser/{id}")]
        public async Task<IActionResult> Put(int id, User user)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (id != user.Id)
            {
                return BadRequest();
            }

            _ClinicaContext.Entry(user).State = EntityState.Modified;

            try
            {
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        [HttpPost("CreateUser")]
        public async Task<ActionResult<User>> Post(User user)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Users == null)
            {
                return Problem("Entity set 'ClinicaContext.User' is null.");
            }
            _ClinicaContext.Users.Add(user);
            await _ClinicaContext.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            if (_ClinicaContext.Users == null)
            {
                return NotFound();
            }
            var user = await _ClinicaContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _ClinicaContext.Users.Remove(user);
            await _ClinicaContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            return (_ClinicaContext.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

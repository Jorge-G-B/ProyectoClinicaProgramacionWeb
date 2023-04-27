using APIClinica.Models;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using ClinicaModels;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("Rol")]
    public class RoleController : Controller
    {
        [Route("GetRolesList")]
        [HttpGet]
        public async Task<IEnumerable<Rol>> GetList()
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            IEnumerable<Rol> roles = _ClinicaContext.Roles.Select(s =>
            new Rol
            {
                Id = s.Id,
                Description = s.Description
            }
            ).ToList();
            return roles;
        }

        [Route("GetRol")]
        [HttpGet]
        public Rol Get(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            Rol rol = _ClinicaContext.Roles.Select(s =>
            new Rol
            {
                Id = s.Id,
                Description = s.Description
            }
            ).FirstOrDefault(s => s.Id == id);
            return rol;
        }

        [Route("CreateRol")]
        [HttpPost]
        public async void Create(Rol rol)
        {
            try
            {
                DbclinicaContext _ClinicaContext = new DbclinicaContext();
                Role _rol = new Role
                {
                    Description = rol.Description
                };
                _ClinicaContext.Roles.Add(_rol);
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        [Route("DeleteRol")]
        [HttpDelete]
        public async void Delete(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    short ID = (short)id;
                    DbclinicaContext _ClinicaContext = new DbclinicaContext();
                    var rol = await _ClinicaContext.Roles.FindAsync(ID);
                    if (rol != null)
                    {
                        _ClinicaContext.Roles.Remove(rol);
                    }
                    await _ClinicaContext.SaveChangesAsync();
                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    // Revertir la transacción
                    transaction.Dispose();
                    throw;
                }
            }
        }

        [Route("EditRol")]
        [HttpPut]
        public async void Edit(Rol rol)
        {
            try
            {
                DbclinicaContext _ClinicaContext = new DbclinicaContext();
                Role _rol = new Role
                {
                    Id = rol.Id,
                    Description = rol.Description
                };
                _ClinicaContext.Roles.Update(_rol);
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}

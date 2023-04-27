using ClinicaModels;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using ClinicaModels;
using APIClinica.Models;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : Controller
    {
        [Route("GetUsersList")]
        [HttpGet]
        public async Task<IEnumerable<Usuario>> GetList()
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            IEnumerable<Usuario> usuarios = _ClinicaContext.Users.Select(s =>
            new Usuario
            {
                Id = s.Id,
                User1 = s.User1,
                Correo = s.Correo,
                Rol = s.Rol
            }
            ).ToList();
            return usuarios;
        }

        [Route("GetUser")]
        [HttpGet]
        public Usuario Get(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            Usuario usuario = _ClinicaContext.Users.Select(s =>
            new Usuario
            {
                Id = s.Id,
                User1 = s.User1,
                Correo = s.Correo,
                Rol = s.Rol,
                Contraseña = s.Contraseña
            }
            ).FirstOrDefault(s => s.Id == id);
            return usuario;
        }

        [Route("CreateUser")]
        [HttpPost]
        public async void Create(Usuario user)
        {
            try
            {
                DbclinicaContext _ClinicaContext = new DbclinicaContext();
                User _user = new User
                {
                    User1 = user.User1,
                    Correo = user.Correo,
                    Contraseña = user.Contraseña,
                    Rol = user.Rol
                };
                _ClinicaContext.Users.Add(_user);
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        [Route("DeleteUser")]
        [HttpDelete]
        public async void Delete(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    DbclinicaContext _ClinicaContext = new DbclinicaContext();
                    var user = await _ClinicaContext.Users.FindAsync(id);
                    if (user != null)
                    {
                        _ClinicaContext.Users.Remove(user);
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

        [Route("EditUser")]
        [HttpPut]
        public async void Edit(Usuario user)
        {
            try
            {
                DbclinicaContext _ClinicaContext = new DbclinicaContext();
                User _user = new User
                {
                    Id = user.Id,
                    User1 = user.User1,
                    Correo = user.Correo,
                    Contraseña = user.Contraseña,
                    Rol = user.Rol
                };
                _ClinicaContext.Users.Update(_user);
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}

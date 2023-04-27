using ClinicaModels;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using ClinicaModels;
using APIClinica.Models;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("Paciente")]
    public class PacienteController : Controller
    {
        [Route("GetPacientesList")]
        [HttpGet]
        public async Task<IEnumerable<ClinicaModels.Paciente>> GetList()
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            IEnumerable<ClinicaModels.Paciente> pacientes = _ClinicaContext.Pacientes.Select(s =>
            new ClinicaModels.Paciente
            {
                Nombre = string.Format("{0} {1} {2} {3}", s.Pnombre, s.Snombre, s.Papellido, s.Sapellido),
                Pnombre = s.Pnombre,
                Snombre = s.Snombre,
                Papellido = s.Papellido,
                Sapellido = s.Sapellido,
                Edad = s.Edad,
                Telefono = s.Telefono,
                FechaDeNacimiento = s.FechaDeNacimiento,
                Email = s.Email,
                Sexo = s.Sexo,
                NombreResponsable = s.NombreResponsable,
                TelResponsable = s.TelResponsable
            }
            ).ToList();
            return pacientes;
        }

        [Route("GetPaciente")]
        [HttpGet]
        public ClinicaModels.Paciente Get(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            ClinicaModels.Paciente usuario = _ClinicaContext.Pacientes.Select(s =>
            new ClinicaModels.Paciente
            {
                Id = s.Id,
                Nombre = string.Format("{0} {1} {2} {3}", s.Pnombre, s.Snombre, s.Papellido, s.Sapellido),
                Pnombre = s.Pnombre,
                Snombre = s.Snombre,
                Papellido = s.Papellido,
                Sapellido = s.Sapellido,
                Edad = s.Edad,
                Telefono = s.Telefono,
                FechaDeNacimiento = s.FechaDeNacimiento,
                Email = s.Email,
                Sexo = s.Sexo,
                NombreResponsable = s.NombreResponsable,
                TelResponsable = s.TelResponsable
            }
            ).FirstOrDefault(s => s.Id == id);
            return usuario;
        }

        [Route("CreatePaciente")]
        [HttpPost]
        public async void Create(ClinicaModels.Paciente paciente)
        {
            try
            {
                DbclinicaContext _ClinicaContext = new DbclinicaContext();
                Models.Paciente _paciente = new Models.Paciente
                {
                    Pnombre = paciente.Pnombre,
                    Snombre = paciente.Snombre,
                    Papellido = paciente.Papellido,
                    Sapellido = paciente.Sapellido,
                    Edad = paciente.Edad,
                    Telefono = paciente.Telefono,
                    FechaDeNacimiento = paciente.FechaDeNacimiento,
                    Email = paciente.Email,
                    Sexo = paciente.Sexo,
                    NombreResponsable = paciente.NombreResponsable,
                    TelResponsable = paciente.TelResponsable
                };
                _ClinicaContext.Pacientes.Add(_paciente);
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        [Route("DeletePaciente")]
        [HttpDelete]
        public async void Delete(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    DbclinicaContext _ClinicaContext = new DbclinicaContext();
                    var paciente = await _ClinicaContext.Pacientes.FindAsync(id);
                    if (paciente != null)
                    {
                        _ClinicaContext.Pacientes.Remove(paciente);
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

        [Route("EditPaciente")]
        [HttpPut]
        public async void Edit(ClinicaModels.Paciente paciente)
        {
            try
            {
                DbclinicaContext _ClinicaContext = new DbclinicaContext();
                Models.Paciente _paciente = new Models.Paciente
                {
                    Id = paciente.Id,
                    Pnombre = paciente.Pnombre,
                    Snombre = paciente.Snombre,
                    Papellido = paciente.Papellido,
                    Sapellido = paciente.Sapellido,
                    Edad = paciente.Edad,
                    Telefono = paciente.Telefono,
                    FechaDeNacimiento = paciente.FechaDeNacimiento,
                    Email = paciente.Email,
                    Sexo = paciente.Sexo,
                    NombreResponsable = paciente.NombreResponsable,
                    TelResponsable = paciente.TelResponsable
                };
                _ClinicaContext.Pacientes.Update(_paciente);
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}

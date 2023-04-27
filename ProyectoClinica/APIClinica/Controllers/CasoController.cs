using ClinicaModels;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using ClinicaModels;
using APIClinica.Models;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("Caso")]
    public class CasoController : Controller
    {
        [Route("GetCasoList")]
        [HttpGet]
        public async Task<IEnumerable<ClinicaModels.Caso>> GetList()
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            IEnumerable<ClinicaModels.Caso> casos = _ClinicaContext.Casos.Select(s =>
            new ClinicaModels.Caso
            {
               FechaDeApertura = s.FechaDeApertura,
               UsuarioCrea = s.UsuarioCrea,
               Idpaciente = s.Idpaciente,
               MotivoConsulta = s.MotivoConsulta,
               Antecedentes = s.Antecedentes,
               Diagnostico = s.Diagnostico,
               ReferidoPor = s.ReferidoPor,
               Estado = s.Estado,
               FechaDeCierre = s.FechaDeCierre,
               MotivoDeCierre = s.MotivoDeCierre
            }
            ).ToList();
            return casos;
        }

        [Route("GetCaso")]
        [HttpGet]
        public ClinicaModels.Caso Get(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            ClinicaModels.Caso caso = _ClinicaContext.Casos.Select(s =>
            new ClinicaModels.Caso
            {
                FechaDeApertura = s.FechaDeApertura,
                UsuarioCrea = s.UsuarioCrea,
                Idpaciente = s.Idpaciente,
                MotivoConsulta = s.MotivoConsulta,
                Antecedentes = s.Antecedentes,
                Diagnostico = s.Diagnostico,
                ReferidoPor = s.ReferidoPor,
                Estado = s.Estado,
                FechaDeCierre = s.FechaDeCierre,
                MotivoDeCierre = s.MotivoDeCierre
            }
            ).FirstOrDefault(s => s.Id == id);
            return caso;
        }

        [Route("CreateCaso")]
        [HttpPost]
        public async void Create(ClinicaModels.Caso caso)
        {
            try
            {
                DbclinicaContext _ClinicaContext = new DbclinicaContext();
                Models.Caso _caso = new Models.Caso
                {
                    FechaDeApertura = caso.FechaDeApertura,
                    UsuarioCrea = caso.UsuarioCrea,
                    Idpaciente = caso.Idpaciente,
                    MotivoConsulta = caso.MotivoConsulta,
                    Antecedentes = caso.Antecedentes,
                    Diagnostico = caso.Diagnostico,
                    ReferidoPor = caso.ReferidoPor,
                    Estado = caso.Estado,
                    FechaDeCierre = caso.FechaDeCierre,
                    MotivoDeCierre = caso.MotivoDeCierre
                };
                _ClinicaContext.Casos.Add(_caso);
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        [Route("DeleteCaso")]
        [HttpDelete]
        public async void Delete(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    DbclinicaContext _ClinicaContext = new DbclinicaContext();
                    var caso = await _ClinicaContext.Casos.FindAsync(id);
                    if (caso != null)
                    {
                        _ClinicaContext.Casos.Remove(caso);
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

        [Route("EditCaso")]
        [HttpPut]
        public async void Edit(ClinicaModels.Caso caso)
        {
            try
            {
                DbclinicaContext _ClinicaContext = new DbclinicaContext();
                Models.Caso _caso = new Models.Caso
                {

                    Id = caso.Id,
                    FechaDeApertura = caso.FechaDeApertura,
                    UsuarioCrea = caso.UsuarioCrea,
                    Idpaciente = caso.Idpaciente,
                    MotivoConsulta = caso.MotivoConsulta,
                    Antecedentes = caso.Antecedentes,
                    Diagnostico = caso.Diagnostico,
                    ReferidoPor = caso.ReferidoPor,
                    Estado = caso.Estado,
                    FechaDeCierre = caso.FechaDeCierre,
                    MotivoDeCierre = caso.MotivoDeCierre
                };
                _ClinicaContext.Casos.Update(_caso);
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}

using ClinicaModels;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using ClinicaModels;
using APIClinica.Models;

namespace APIClinica.Controllers
{
    [ApiController]
    [Route("Consulta")]
    public class ConsultaController : Controller
    {
        [Route("GetConsultaList")]
        [HttpGet]
        public async Task<IEnumerable<Consulta>> GetList()
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            IEnumerable<Consulta> consultas = _ClinicaContext.Consulta.Select(s =>
            new Consulta
            {
                Idcaso = s.Idcaso,
                FechaDeConsulta = s.FechaDeConsulta,
                DatosSubjetivos = s.DatosSubjetivos,
                DatosObjetivos = s.DatosObjetivos,
                PlanTerapuetico = s.PlanTerapuetico,
                NuevosDatos = s.NuevosDatos,
                Estado = s.Estado
            }
            ).ToList();
            return consultas;
        }

        [Route("GetConsulta")]
        [HttpGet]
        public Consulta Get(int id)
        {
            DbclinicaContext _ClinicaContext = new DbclinicaContext();
            Consulta consulta = _ClinicaContext.Consulta.Select(s =>
            new Consulta
            {
                 Idcaso = s.Idcaso,
                 FechaDeConsulta = s.FechaDeConsulta,
                 DatosSubjetivos  = s.DatosSubjetivos,
                 DatosObjetivos  = s.DatosObjetivos,
                 PlanTerapuetico = s.PlanTerapuetico,
                 NuevosDatos = s.NuevosDatos,
                 Estado = s.Estado
            }).FirstOrDefault(s => s.Id == id);
            return consulta;
        }

        [Route("CreateConsulta")]
        [HttpPost]
        public async void Create(Consulta consulta)
        {
            try
            {
                DbclinicaContext _ClinicaContext = new DbclinicaContext();
                Consultum _consulta = new Consultum
                {
                    Idcaso = consulta.Idcaso,
                    FechaDeConsulta = consulta.FechaDeConsulta,
                    DatosSubjetivos = consulta.DatosSubjetivos,
                    DatosObjetivos = consulta.DatosObjetivos,
                    PlanTerapuetico = consulta.PlanTerapuetico,
                    NuevosDatos = consulta.NuevosDatos,
                    Estado = consulta.Estado
                };
                _ClinicaContext.Consulta.Add(_consulta);
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        [Route("DeleteConsulta")]
        [HttpDelete]
        public async void Delete(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    DbclinicaContext _ClinicaContext = new DbclinicaContext();
                    var consulta = await _ClinicaContext.Consulta.FindAsync(id);
                    if (consulta != null)
                    {
                        _ClinicaContext.Consulta.Remove(consulta);
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

        [Route("EditConsulta")]
        [HttpPut]
        public async void Edit(Consulta consulta)
        {
            try
            {
                DbclinicaContext _ClinicaContext = new DbclinicaContext();
                Consultum _consulta = new Consultum
                {
                    Id = consulta.Id,
                    Idcaso = consulta.Idcaso,
                    FechaDeConsulta = consulta.FechaDeConsulta,
                    DatosSubjetivos = consulta.DatosSubjetivos,
                    DatosObjetivos = consulta.DatosObjetivos,
                    PlanTerapuetico = consulta.PlanTerapuetico,
                    NuevosDatos = consulta.NuevosDatos,
                    Estado = consulta.Estado
                };
                _ClinicaContext.Consulta.Update(_consulta);
                await _ClinicaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}

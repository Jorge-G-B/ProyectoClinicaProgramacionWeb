using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoClinica.Models;
using ProyectoClinica.Services;

namespace ProyectoClinica.Controllers
{
    public class ConsultumsController : Controller
    {
        private readonly DbclinicaContext _context;

        public ConsultumsController()
        {
            _context = new DbclinicaContext();
        }

        [Authorize]
        // GET: Consultums
        public async Task<IActionResult> Index()
        {
            IEnumerable<Consulta> consultas = await APIServices.GetConsultations();
            foreach (var consulta in consultas)
            {
                consulta.IdcasoNavigation = await APIServices.GetCase(consulta.Idcaso);
                consulta.IdcasoNavigation.IdpacienteNavigation = await APIServices.GetPacient(consulta.IdcasoNavigation.Idpaciente);
                consulta.IdcasoNavigation.IdpacienteNavigation.NombreC = consulta.IdcasoNavigation.IdpacienteNavigation.Pnombre + " " + consulta.IdcasoNavigation.IdpacienteNavigation.Papellido;

            }
            return View(consultas);
        }

        // GET: Consultums/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            Consulta consulta = await APIServices.GetConsultation(id);
            consulta.IdcasoNavigation = await APIServices.GetCase(consulta.Idcaso);
            consulta.IdcasoNavigation.IdpacienteNavigation = await APIServices.GetPacient(consulta.IdcasoNavigation.Idpaciente);
            consulta.IdcasoNavigation.IdpacienteNavigation.NombreC = consulta.IdcasoNavigation.IdpacienteNavigation.Pnombre + " " + consulta.IdcasoNavigation.IdpacienteNavigation.Papellido;
            return View(consulta);
        }
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var cases = await APIServices.GetCases();
            ViewData["Idcaso"] = new SelectList(cases, "Id", "Id");
            return View();
        }

        // POST: Consultums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(Consulta consulta)
        {
            consulta.Estado = "Activo";
            await APIServices.CreateConsultation(consulta);
            return RedirectToAction(nameof(Index));
        }

        // GET: Consultums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var consulta = await APIServices.GetConsultation(id);
            var cases = await APIServices.GetCases();
            ViewData["Idcaso"] = new SelectList(cases, "Id", "Id");
            return View(consulta);
        }

        // POST: Consultums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(Consulta consultum)
        {
            await APIServices.EditConsultation(consultum);
            return RedirectToAction(nameof(Index));
        }

        // GET: Consultums/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            Consulta consulta = await APIServices.GetConsultation(id);
            consulta.IdcasoNavigation = await APIServices.GetCase(consulta.Idcaso);
            return View(consulta);
        }

        // POST: Consultums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await APIServices.DeleteConsultation(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> GetConsultaJson()
        {
            int id = Convert.ToInt32(HttpContext.Request.Form["consultaId"].FirstOrDefault().ToString());
            var rol = await APIServices.GetConsultation(id);
            var jsonresult = new { rol };
            return Json(jsonresult);
        }
    }
}

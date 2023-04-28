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
    public class CasoesController : Controller
    {
        private readonly DbclinicaContext _context;

        public CasoesController()
        {
            _context = new DbclinicaContext();
        }

        [Authorize]
        // GET: Casoes
        public async Task<IActionResult> Index()
        {
            IEnumerable<ClinicaModels.Caso> cases = await APIServices.GetCases();
            foreach (var caso in cases)
            {
                caso.UsuarioCreaNavigation = await APIServices.GetUser(caso.UsuarioCrea);
                caso.IdpacienteNavigation = await APIServices.GetPacient(caso.Idpaciente);
                caso.IdpacienteNavigation.NombreC = caso.IdpacienteNavigation.Pnombre + " " + caso.IdpacienteNavigation.Papellido;
                caso.IdpacienteNavigation.NombreF = caso.IdpacienteNavigation.Pnombre + " " + caso.IdpacienteNavigation.Snombre +
                     " " + caso.IdpacienteNavigation.Papellido + " " + caso.IdpacienteNavigation.Sapellido;
            }
            return View(cases);
        }

        // GET: Casoes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            ClinicaModels.Caso caso = await APIServices.GetCase(id);
            caso.UsuarioCreaNavigation = await APIServices.GetUser(caso.UsuarioCrea);
            caso.IdpacienteNavigation = await APIServices.GetPacient(caso.Idpaciente);
            caso.IdpacienteNavigation.NombreC = caso.IdpacienteNavigation.Pnombre + " " + caso.IdpacienteNavigation.Papellido;
            caso.IdpacienteNavigation.NombreF = caso.IdpacienteNavigation.Pnombre + " " + caso.IdpacienteNavigation.Snombre +
                 " " + caso.IdpacienteNavigation.Papellido + " " + caso.IdpacienteNavigation.Sapellido;
            return View(caso);
        }

        // GET: Casoes/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var pacients = await APIServices.GetPacients();
            foreach (var paciente in pacients)
            {
                paciente.NombreC = paciente.Pnombre + " " + paciente.Papellido;
            }
            ViewData["Idpaciente"] = new SelectList(pacients, "Id", "NombreC");
            return View();
        }

        // POST: Casoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(ClinicaModels.Caso caso)
        {
            caso.Estado = "Activo";
            caso.MotivoDeCierre = " ";
            caso.UsuarioCrea = 6;
            await APIServices.CreateCase(caso);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var caso = await APIServices.GetCase(id);
            var pacients = await APIServices.GetPacients();
            foreach (var paciente in pacients)
            {
                paciente.NombreC = paciente.Pnombre + " " + paciente.Papellido;
            }
            ViewData["Idpaciente"] = new SelectList(pacients, "Id", "NombreC");
            return View(caso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(ClinicaModels.Caso caso)
        {
            caso.UsuarioCrea = 6;
            await APIServices.EditCase(caso);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            ClinicaModels.Caso caso = await APIServices.GetCase(id);
            var pacients = await APIServices.GetPacients();
            foreach (var paciente in pacients)
            {
                paciente.NombreF = paciente.Pnombre + " " + paciente.Snombre +
                 " " + paciente.Papellido + " " + paciente.Sapellido; ;
            }
            return View(caso);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await APIServices.DeleteCase(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> GetCaseJson()
        {
            int id = (short)Convert.ToInt32(HttpContext.Request.Form["caseId"].FirstOrDefault().ToString());
            var rol = await APIServices.GetCase(id);
            var jsonresult = new { rol };
            return Json(jsonresult);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoClinica.Models;
using ClinicaModels;
using NuGet.Protocol.Plugins;
using ProyectoClinica.Services;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoClinica.Controllers
{
    public class PacientesController : Controller
    {
        private readonly DbclinicaContext _context;

        public PacientesController()
        {
            _context = new DbclinicaContext();
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ClinicaModels.Paciente> pacient = await APIServices.GetPacients();
            return View(pacient);
        }

        // GET: Roles/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var pacient = await APIServices.GetPacient(id);
            return View(pacient);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClinicaModels.Paciente pacient)
        {
            await APIServices.CreatePacient(pacient);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var pacient = await APIServices.GetPacient(id);
            return View(pacient);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClinicaModels.Paciente pacient)
        {
            await APIServices.EditPacient(pacient);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var rol = await APIServices.GetPacient(id);
            return View(rol);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            await APIServices.DeletePacient(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost]
        public async Task<JsonResult> GetRoleJson()
        {
            int id = Convert.ToInt32(HttpContext.Request.Form["pacientId"].FirstOrDefault().ToString());
            var rol = await APIServices.GetPacient(id);
            var jsonresult = new { rol };
            return Json(jsonresult);
        }
    }
}

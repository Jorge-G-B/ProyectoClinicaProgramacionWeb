using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ProyectoClinica.Models;
using ClinicaModels;
using ProyectoClinica.Services;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoClinica.Controllers
{
    public class RolesController : Controller
    {
        private readonly DbclinicaContext _context;

        public RolesController()
        {
            _context = new DbclinicaContext();
        }

        // GET: Roles
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Rol> roles = await APIServices.GetRoles();
            return View(roles);
        }

        [Authorize]
        // GET: Roles/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            var rol = await APIServices.GetRole(id);
            return View(rol);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Description")] Rol rol)
        {
            await APIServices.CreateRole(rol);
            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public async Task<IActionResult> Edit(short? id)
        {
            var rol = await APIServices.GetRole(id);
            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Rol rol)
        {
            await APIServices.EditRole(rol);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Delete(short? id)
        {
            var rol = await APIServices.GetRole(id);
            return View(rol);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            await APIServices.DeleteRole(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> GetRoleJson()
        {
            short id = (short)Convert.ToInt32(HttpContext.Request.Form["roleId"].FirstOrDefault().ToString());
            var rol = await APIServices.GetRole(id);
            var jsonresult = new { rol };
            return Json(jsonresult);
        }
    }
}

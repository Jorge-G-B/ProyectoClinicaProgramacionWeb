using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using ProyectoClinica.Models;
using ProyectoClinica.Services;
using ClinicaModels;
using Microsoft.AspNetCore.Authorization;

namespace ProyectoClinica.Controllers
{
    public class UsersController : Controller
    {
        private readonly DbclinicaContext _context;


        public UsersController()
        {
            _context = new DbclinicaContext();
        }

        [Authorize]
        // GET: Users
        public async Task<IActionResult> Index()
        {
            IEnumerable<Usuario> users = await APIServices.GetUsers();
            foreach (var user in users)
            {
                user.RolNavigation = await APIServices.GetRole(user.Rol);
            }
            return View(users);
        }

        // GET: Users/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            Usuario user = await APIServices.GetUser(id);
            user.RolNavigation = await APIServices.GetRole(user.Rol);
            return View(user);
        }
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var roles = await APIServices.GetRoles();
            ViewData["Rol"] = new SelectList(roles, "Id", "Description");
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Usuario user)
        {
            await APIServices.CreateUser(user);
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await APIServices.GetUser(id);
            var roles = await APIServices.GetRoles();
            ViewData["Rol"] = new SelectList(roles, "Id", "Description");
            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Usuario user)
        {
            await APIServices.EditUser(user);
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            Usuario user = await APIServices.GetUser(id);
            user.RolNavigation = await APIServices.GetRole(user.Rol);
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await APIServices.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<JsonResult> GetUserJson()
        {
            short id = (short)Convert.ToInt32(HttpContext.Request.Form["userId"].FirstOrDefault().ToString());
            var rol = await APIServices.GetUser(id);
            var jsonresult = new { rol };
            return Json(jsonresult);
        }
    }
}

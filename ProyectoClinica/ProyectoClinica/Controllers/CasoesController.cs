using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoClinica.Models;

namespace ProyectoClinica.Controllers
{
    public class CasoesController : Controller
    {
        private readonly DbclinicaContext _context;

        public CasoesController()
        {
            _context = new DbclinicaContext();
        }

        // GET: Casoes
        public async Task<IActionResult> Index()
        {
            var dbclinicaContext = _context.Casos.Include(c => c.IdpacienteNavigation).Include(c => c.UsuarioCreaNavigation);
            return View(await dbclinicaContext.ToListAsync());
        }

        // GET: Casoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Casos == null)
            {
                return NotFound();
            }

            var caso = await _context.Casos
                .Include(c => c.IdpacienteNavigation)
                .Include(c => c.UsuarioCreaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caso == null)
            {
                return NotFound();
            }

            return View(caso);
        }

        // GET: Casoes/Create
        public IActionResult Create()
        {
            ViewData["Idpaciente"] = new SelectList(_context.Pacientes, "Id", "Id");
            ViewData["UsuarioCrea"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Casoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaDeApertura,UsuarioCrea,Idpaciente,MotivoConsulta,Antecedentes,Diagnostico,ReferidoPor,Estado,FechaDeCierre,MotivoDeCierre")] Caso caso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(caso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idpaciente"] = new SelectList(_context.Pacientes, "Id", "Id", caso.Idpaciente);
            ViewData["UsuarioCrea"] = new SelectList(_context.Users, "Id", "Id", caso.UsuarioCrea);
            return View(caso);
        }

        // GET: Casoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Casos == null)
            {
                return NotFound();
            }

            var caso = await _context.Casos.FindAsync(id);
            if (caso == null)
            {
                return NotFound();
            }
            ViewData["Idpaciente"] = new SelectList(_context.Pacientes, "Id", "Id", caso.Idpaciente);
            ViewData["UsuarioCrea"] = new SelectList(_context.Users, "Id", "Id", caso.UsuarioCrea);
            return View(caso);
        }

        // POST: Casoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FechaDeApertura,UsuarioCrea,Idpaciente,MotivoConsulta,Antecedentes,Diagnostico,ReferidoPor,Estado,FechaDeCierre,MotivoDeCierre")] Caso caso)
        {
            if (id != caso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(caso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CasoExists(caso.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idpaciente"] = new SelectList(_context.Pacientes, "Id", "Id", caso.Idpaciente);
            ViewData["UsuarioCrea"] = new SelectList(_context.Users, "Id", "Id", caso.UsuarioCrea);
            return View(caso);
        }

        // GET: Casoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Casos == null)
            {
                return NotFound();
            }

            var caso = await _context.Casos
                .Include(c => c.IdpacienteNavigation)
                .Include(c => c.UsuarioCreaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (caso == null)
            {
                return NotFound();
            }

            return View(caso);
        }

        // POST: Casoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Casos == null)
            {
                return Problem("Entity set 'DbclinicaContext.Casos'  is null.");
            }
            var caso = await _context.Casos.FindAsync(id);
            if (caso != null)
            {
                _context.Casos.Remove(caso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CasoExists(int id)
        {
          return (_context.Casos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

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
    public class ConsultumsController : Controller
    {
        private readonly DbclinicaContext _context;

        public ConsultumsController()
        {
            _context = new DbclinicaContext();
        }

        // GET: Consultums
        public async Task<IActionResult> Index()
        {
            var dbclinicaContext = _context.Consulta.Include(c => c.IdcasoNavigation);
            return View(await dbclinicaContext.ToListAsync());
        }

        // GET: Consultums/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Consulta == null)
            {
                return NotFound();
            }

            var consultum = await _context.Consulta
                .Include(c => c.IdcasoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultum == null)
            {
                return NotFound();
            }

            return View(consultum);
        }

        // GET: Consultums/Create
        public IActionResult Create()
        {
            ViewData["Idcaso"] = new SelectList(_context.Casos, "Id", "Id");
            return View();
        }

        // POST: Consultums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idcaso,FechaDeConsulta,DatosSubjetivos,DatosObjetivos,PlanTerapuetico,NuevosDatos,Estado")] Consultum consultum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Idcaso"] = new SelectList(_context.Casos, "Id", "Id", consultum.Idcaso);
            return View(consultum);
        }

        // GET: Consultums/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Consulta == null)
            {
                return NotFound();
            }

            var consultum = await _context.Consulta.FindAsync(id);
            if (consultum == null)
            {
                return NotFound();
            }
            ViewData["Idcaso"] = new SelectList(_context.Casos, "Id", "Id", consultum.Idcaso);
            return View(consultum);
        }

        // POST: Consultums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Idcaso,FechaDeConsulta,DatosSubjetivos,DatosObjetivos,PlanTerapuetico,NuevosDatos,Estado")] Consultum consultum)
        {
            if (id != consultum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultumExists(consultum.Id))
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
            ViewData["Idcaso"] = new SelectList(_context.Casos, "Id", "Id", consultum.Idcaso);
            return View(consultum);
        }

        // GET: Consultums/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Consulta == null)
            {
                return NotFound();
            }

            var consultum = await _context.Consulta
                .Include(c => c.IdcasoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultum == null)
            {
                return NotFound();
            }

            return View(consultum);
        }

        // POST: Consultums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Consulta == null)
            {
                return Problem("Entity set 'DbclinicaContext.Consulta'  is null.");
            }
            var consultum = await _context.Consulta.FindAsync(id);
            if (consultum != null)
            {
                _context.Consulta.Remove(consultum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultumExists(long id)
        {
          return (_context.Consulta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

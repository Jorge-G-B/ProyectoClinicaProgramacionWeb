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

namespace ProyectoClinica.Controllers
{
    public class PacientesController : Controller
    {
        private readonly DbclinicaContext _context;

        public PacientesController()
        {
            _context = new DbclinicaContext();
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            var listPacientes = _context.Pacientes;
            if(listPacientes != null)
            {
                List<ClinicaModels.Paciente> pacientes = new List<ClinicaModels.Paciente>();
                foreach(Models.Paciente paciente in listPacientes)
                {
                    pacientes.Add(new ClinicaModels.Paciente()
                    {
                        Pnombre = paciente.Pnombre,
                        Snombre = paciente.Snombre,
                        Papellido = paciente.Papellido,
                        Sapellido = paciente.Papellido,
                        Nombre = string.Format("{0} {1} {2} {3}", paciente.Pnombre, paciente.Snombre, paciente.Papellido, paciente.Sapellido),
                        Sexo = paciente.Sexo,
                        Edad = paciente.Edad,
                        Telefono = paciente.Telefono,
                        Email = paciente.Email,
                        FechaDeNacimiento = paciente.FechaDeNacimiento,
                        NombreResponsable = paciente.NombreResponsable,
                        TelResponsable = paciente.TelResponsable,
                    });
                }
                return View(pacientes);
            }
            return Problem("Entity set 'DbclinicaContext.Pacientes'  is null.");

                          
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClinicaModels.Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                Models.Paciente _paciente = new Models.Paciente()
                {
                    Pnombre = paciente.Pnombre,
                    Snombre = paciente.Snombre,
                    Papellido = paciente.Papellido,
                    Sapellido = paciente.Papellido,
                    Sexo = paciente.Sexo,
                    Edad = (short)paciente.Edad,
                    Telefono = paciente.Telefono,
                    Email = paciente.Email,
                    FechaDeNacimiento = paciente.FechaDeNacimiento,
                    NombreResponsable = paciente.NombreResponsable,
                    TelResponsable = paciente.TelResponsable,
                };
                _context.Add(_paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClinicaModels.Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Models.Paciente _paciente = new Models.Paciente()
                    {
                        Pnombre = paciente.Pnombre,
                        Snombre = paciente.Snombre,
                        Papellido = paciente.Papellido,
                        Sapellido = paciente.Papellido,
                        Sexo = paciente.Sexo,
                        Edad = (short)paciente.Edad,
                        Telefono = paciente.Telefono,
                        Email = paciente.Email,
                        FechaDeNacimiento = paciente.FechaDeNacimiento,
                        NombreResponsable = paciente.NombreResponsable,
                        TelResponsable = paciente.TelResponsable,
                    };
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pacientes == null)
            {
                return Problem("Entity set 'DbclinicaContext.Pacientes'  is null.");
            }
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
          return (_context.Pacientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

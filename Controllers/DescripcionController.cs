using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sistema_servicio_tecnico.Models;

namespace sistema_servicio_tecnico.Controllers
{
    [Authorize]
    public class DescripcionController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public DescripcionController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: Descripcion
        public async Task<IActionResult> Index()
        {
            var mercyDeveloperContext = _context.DescripcionServicios.Include(d => d.Servicio);
            return View(await mercyDeveloperContext.ToListAsync());
        }

        // GET: Descripcion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DescripcionServicios == null)
            {
                return NotFound();
            }

            var descripcionServicio = await _context.DescripcionServicios
                .Include(d => d.Servicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (descripcionServicio == null)
            {
                return NotFound();
            }

            return View(descripcionServicio);
        }

        // GET: Descripcion/Create
        public IActionResult Create()
        {
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id");
            return View();
        }

        // POST: Descripcion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,ServicioId")] DescripcionServicio descripcionServicio)
        {
            if (descripcionServicio.Nombre != null && descripcionServicio.ServicioId != 0)
            {
                _context.Add(descripcionServicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id", descripcionServicio.ServicioId);
            return View(descripcionServicio);
        }

        // GET: Descripcion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DescripcionServicios == null)
            {
                return NotFound();
            }

            var descripcionServicio = await _context.DescripcionServicios.FindAsync(id);
            if (descripcionServicio == null)
            {
                return NotFound();
            }
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id", descripcionServicio.ServicioId);
            return View(descripcionServicio);
        }

        // POST: Descripcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,ServicioId")] DescripcionServicio descripcionServicio)
        {
            if (id != descripcionServicio.Id)
            {
                return NotFound();
            }

            if (descripcionServicio.Nombre != null && descripcionServicio.ServicioId != 0)
            {
                try
                {
                    _context.Update(descripcionServicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescripcionServicioExists(descripcionServicio.Id))
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
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id", descripcionServicio.ServicioId);
            return View(descripcionServicio);
        }

        // GET: Descripcion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DescripcionServicios == null)
            {
                return NotFound();
            }

            var descripcionServicio = await _context.DescripcionServicios
                .Include(d => d.Servicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (descripcionServicio == null)
            {
                return NotFound();
            }

            return View(descripcionServicio);
        }

        // POST: Descripcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DescripcionServicios == null)
            {
                return Problem("Entity set 'MercyDeveloperContext.DescripcionServicios'  is null.");
            }
            var descripcionServicio = await _context.DescripcionServicios.FindAsync(id);
            if (descripcionServicio != null)
            {
                _context.DescripcionServicios.Remove(descripcionServicio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescripcionServicioExists(int id)
        {
          return (_context.DescripcionServicios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

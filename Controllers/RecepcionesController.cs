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

    public class RecepcionesController : Controller
    {
        private readonly MercyDeveloperContext _context;

        public RecepcionesController(MercyDeveloperContext context)
        {
            _context = context;
        }

        // GET: Recepciones
        public async Task<IActionResult> Index()
        {
            var mercyDeveloperContext = _context.RecepcionEquipos.Include(r => r.Cliente).Include(r => r.Servicio);
            return View(await mercyDeveloperContext.ToListAsync());
        }

        // GET: Recepciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RecepcionEquipos == null)
            {
                return NotFound();
            }

            var recepcionEquipo = await _context.RecepcionEquipos
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recepcionEquipo == null)
            {
                return NotFound();
            }

            return View(recepcionEquipo);
        }

        // GET: Recepciones/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id");
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id");
            return View();
        }

        // POST: Recepciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Accesorio,CapacidadAlmacenamiento,CapacidadRam,ClienteId,Fecha,Grafico,MarcaPc,ModeloPc,Nserie,ServicioId,TipoAlmacenamiento,TipoGpu,TipoPc")] RecepcionEquipo recepcionEquipo)
        {
            if (recepcionEquipo.Fecha != null && recepcionEquipo.CapacidadRam != 0)
            {
                _context.Add(recepcionEquipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", recepcionEquipo.ClienteId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id", recepcionEquipo.ServicioId);
            return View(recepcionEquipo);
        }

        // GET: Recepciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RecepcionEquipos == null)
            {
                return NotFound();
            }

            var recepcionEquipo = await _context.RecepcionEquipos.FindAsync(id);
            if (recepcionEquipo == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", recepcionEquipo.ClienteId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id", recepcionEquipo.ServicioId);
            return View(recepcionEquipo);
        }

        // POST: Recepciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Accesorio,CapacidadAlmacenamiento,CapacidadRam,ClienteId,Fecha,Grafico,MarcaPc,ModeloPc,Nserie,ServicioId,TipoAlmacenamiento,TipoGpu,TipoPc")] RecepcionEquipo recepcionEquipo)
        {
            if (id != recepcionEquipo.Id)
            {
                return NotFound();
            }

            if (recepcionEquipo.Fecha != null && recepcionEquipo.CapacidadRam != 0)
            {
                try
                {
                    _context.Update(recepcionEquipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecepcionEquipoExists(recepcionEquipo.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Id", recepcionEquipo.ClienteId);
            ViewData["ServicioId"] = new SelectList(_context.Servicios, "Id", "Id", recepcionEquipo.ServicioId);
            return View(recepcionEquipo);
        }

        // GET: Recepciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RecepcionEquipos == null)
            {
                return NotFound();
            }

            var recepcionEquipo = await _context.RecepcionEquipos
                .Include(r => r.Cliente)
                .Include(r => r.Servicio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recepcionEquipo == null)
            {
                return NotFound();
            }

            return View(recepcionEquipo);
        }

        // POST: Recepciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RecepcionEquipos == null)
            {
                return Problem("Entity set 'MercyDeveloperContext.RecepcionEquipos'  is null.");
            }
            var recepcionEquipo = await _context.RecepcionEquipos.FindAsync(id);
            if (recepcionEquipo != null)
            {
                _context.RecepcionEquipos.Remove(recepcionEquipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecepcionEquipoExists(int id)
        {
          return (_context.RecepcionEquipos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

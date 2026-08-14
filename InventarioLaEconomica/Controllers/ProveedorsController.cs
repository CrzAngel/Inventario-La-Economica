using InventarioLaEconomica.data;
using InventarioLaEconomica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioLaEconomica.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly InventarioContext _context;

        public ProveedoresController(InventarioContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Proveedores.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Id == id);

            if (proveedor == null)
                return NotFound();

            return View(proveedor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Nombre,RNC,Telefono,Email,Direccion,Activo")]
            Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(proveedor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
                return NotFound();

            return View(proveedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Nombre,RNC,Telefono,Email,Direccion,Activo")]
            Proveedor proveedor)
        {
            if (id != proveedor.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(proveedor);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(proveedor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var proveedor = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.Id == id);

            if (proveedor == null)
                return NotFound();

            return View(proveedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor != null)
                _context.Proveedores.Remove(proveedor);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
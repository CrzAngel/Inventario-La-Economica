using InventarioLaEconomica.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventarioLaEconomica.Controllers
{
    public class ReportesController : Controller
    {
        private readonly InventarioContext _context;

        public ReportesController(InventarioContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Inventario()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .OrderBy(p => p.Nombre)
                .ToListAsync();

            return View(productos);
        }

        public async Task<IActionResult> StockBajo()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Where(p => p.Stock <= 5)
                .OrderBy(p => p.Stock)
                .ToListAsync();

            return View(productos);
        }

        public async Task<IActionResult> Movimientos()
        {
            var movimientos = await _context.MovimientosInventario
                .Include(m => m.Producto)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync();

            return View(movimientos);
        }
    }
}
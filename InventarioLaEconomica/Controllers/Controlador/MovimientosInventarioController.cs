using InventarioLaEconomica.data;
using InventarioLaEconomica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventarioLaEconomica.Controllers
{
    public class MovimientosInventarioController : Controller
    {
        private readonly InventarioContext _context;

        public MovimientosInventarioController(InventarioContext context)
        {
            _context = context;
        }

        // LISTA DE MOVIMIENTOS
        public async Task<IActionResult> Index()
        {
            var movimientos = _context.MovimientosInventario
                .Include(m => m.Producto)
                .OrderByDescending(m => m.Fecha);

            return View(await movimientos.ToListAsync());
        }

        // CREAR MOVIMIENTO
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(
                _context.Productos.Where(p => p.Activo),
                "Id",
                "Nombre");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ProductoId,Tipo,Cantidad,Descripcion")]
            MovimientoInventario movimiento)
        {
            var producto = await _context.Productos
                .FindAsync(movimiento.ProductoId);

            if (producto == null)
            {
                ModelState.AddModelError("", "El producto no existe.");
            }

            if (movimiento.Cantidad <= 0)
            {
                ModelState.AddModelError(
                    "Cantidad",
                    "La cantidad debe ser mayor que 0.");
            }

            if (movimiento.Tipo != "Entrada" &&
                movimiento.Tipo != "Salida")
            {
                ModelState.AddModelError(
                    "Tipo",
                    "Selecciona Entrada o Salida.");
            }

            if (producto != null &&
                movimiento.Tipo == "Salida" &&
                movimiento.Cantidad > producto.Stock)
            {
                ModelState.AddModelError(
                    "Cantidad",
                    "No hay suficiente stock disponible.");
            }

            if (ModelState.IsValid && producto != null)
            {
                if (movimiento.Tipo == "Entrada")
                {
                    producto.Stock += movimiento.Cantidad;
                }
                else if (movimiento.Tipo == "Salida")
                {
                    producto.Stock -= movimiento.Cantidad;
                }

                movimiento.Fecha = DateTime.Now;

                _context.MovimientosInventario.Add(movimiento);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["ProductoId"] = new SelectList(
                _context.Productos.Where(p => p.Activo),
                "Id",
                "Nombre",
                movimiento.ProductoId);

            return View(movimiento);
        }
    }
}
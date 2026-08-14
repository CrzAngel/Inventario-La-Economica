using InventarioLaEconomica.data;
using InventarioLaEconomica.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InventarioLaEconomica.Controllers
{
    public class HomeController : Controller
    {
        private readonly InventarioContext _context;

        public HomeController(InventarioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalProductos = _context.Productos.Count();
            ViewBag.TotalCategorias = _context.Categorias.Count();
            ViewBag.TotalProveedores = _context.Proveedores.Count();
            ViewBag.StockBajo = _context.Productos.Count(p => p.Stock <= 5);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
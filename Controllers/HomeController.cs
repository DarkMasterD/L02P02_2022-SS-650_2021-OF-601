using L02P02_2022_SS_650_2021_OF_601.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace L02P02_2022_SS_650_2021_OF_601.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly libreriaContext _context;

        public HomeController(ILogger<HomeController> logger, libreriaContext context)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult IniciarVenta(clientes cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.created_at = DateTime.Now;
                _context.clientes.Add(cliente);
                _context.SaveChanges();

                var pedido = new pedido_encabezado
                {
                    id_cliente = cliente.id,
                    cantidad_libros = 0, 
                    total = 0.0, 
                    estado = 'P'
                };
                _context.pedido_encabezado.Add(pedido);
                _context.SaveChanges();

                return RedirectToAction("CarritoVenta", "Venta", new { id_pedido = pedido.id });
            }

            return View(cliente);
        }

    }
}

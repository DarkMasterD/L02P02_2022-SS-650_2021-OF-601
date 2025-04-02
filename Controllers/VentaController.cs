using L02P02_2022_SS_650_2021_OF_601.Models;
using Microsoft.AspNetCore.Mvc;

namespace L02P02_2022_SS_650_2021_OF_601.Controllers
{
    public class VentaController : Controller
    {
        private readonly libreriaContext _libreriaContext;
        public VentaController(libreriaContext context) 
        {
            _libreriaContext = context;
        }
        public IActionResult InicioVenta()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CarritoVenta()
        {
            var libros = (from l in _libreriaContext.libros 
                          join a in _libreriaContext.autores
                          on l.id_autor equals a.id
                          select new
                          {
                              id = l.id,
                              libro = l.nombre,
                              autor = a.autor,
                              precio = l.precio,
                          }).ToList();
            ViewData["listadoLibros"] = libros;
            return View();
        }
        [HttpPost]
        public IActionResult CarritoVenta(int id_libro)
        {
            pedido_detalle pedido = new pedido_detalle();
            pedido.id_pedido = 1;
            pedido.id_libro = id_libro;
            pedido.created_at = DateTime.Now;

            _libreriaContext.pedido_detalle.Add(pedido);
            _libreriaContext.SaveChanges();



            return Json(new { success = true });
        }
    }
}

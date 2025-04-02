using L02P02_2022_SS_650_2021_OF_601.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L02P02_2022_SS_650_2021_OF_601.Controllers
{
    public class VentaController : Controller
    {
        private readonly libreriaContext _libreriaContext;
        public VentaController(libreriaContext context) 
        {
            _libreriaContext = context;
        }
        [HttpGet]
        public IActionResult CarritoVenta(int id_pedido, int id_cliente)
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
            ViewBag.IdPedido = id_pedido;
            ViewBag.IdCliente = id_cliente;
            return View();
        }
        [HttpPost]
        public IActionResult CarritoVenta(int id_libro, int id_pedido, string a = "")
        {
            pedido_detalle pedido = new pedido_detalle();
            pedido.id_pedido = id_pedido;
            pedido.id_libro = id_libro;
            pedido.created_at = DateTime.Now;

            _libreriaContext.pedido_detalle.Add(pedido);
            _libreriaContext.SaveChanges();

            pedido_encabezado? ActPedido = (from p in _libreriaContext.pedido_encabezado
                                                 where p.id == id_pedido
                                                 select p).FirstOrDefault();

            ActPedido.cantidad_libros = ActPedido.cantidad_libros + 1;

            _libreriaContext.Entry(ActPedido).State = EntityState.Modified;
            _libreriaContext.SaveChanges();

            return Json(new { success = true });
        }
        [HttpGet]
        public IActionResult InicioVenta(int id_pedido, int id_cliente)
        {
            clientes? Cliente = (from c in _libreriaContext.clientes
                                      where c.id == id_cliente
                                      select c).FirstOrDefault();

            var DetPedido = (from pd in _libreriaContext.pedido_detalle
                             join l in _libreriaContext.libros
                             on pd.id_libro equals l.id
                             join a in _libreriaContext.autores
                             on l.id_autor equals a.id 
                             where pd.id_pedido == id_pedido
                             select new
                             {
                                 libro = l.nombre,
                                 autor = a.autor,
                                 precio = l.precio
                             }).ToList();

            pedido_encabezado? EncPedido = (from ep in _libreriaContext.pedido_encabezado
                                                 where ep.id == id_pedido
                                                 select ep).FirstOrDefault();

            ViewData["DetPedido"] = DetPedido;
            ViewBag.cliente = Cliente;
            ViewBag.id_pedido = id_pedido;
            ViewBag.Total = EncPedido.total;

            return View();
        }
    }
}

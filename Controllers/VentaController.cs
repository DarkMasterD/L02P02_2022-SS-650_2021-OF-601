using Microsoft.AspNetCore.Mvc;

namespace L02P02_2022_SS_650_2021_OF_601.Controllers
{
    public class VentaController : Controller
    {
        public IActionResult InicioVenta()
        {
            return View();
        }
    }
}

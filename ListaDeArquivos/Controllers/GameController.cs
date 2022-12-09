using Filtros;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeArquivos.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

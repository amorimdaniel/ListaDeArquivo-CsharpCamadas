using Filtros;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeArquivos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

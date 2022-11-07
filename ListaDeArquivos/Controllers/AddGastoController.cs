using Data;
using Filtros;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ListaDeArquivos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class AddGastoController : Controller
    {
        private readonly ISessao _sessao;
        private readonly IGastos _gastos;
        public AddGastoController(ISessao sessao, IGastos gastos)
        {
            _sessao = sessao;
            _gastos = gastos;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CadastrarGasto(Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                Usuario usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                TempData["Mensagem"] = _gastos.CadastrarGasto(gasto, usuarioLogado.IdUsuario);
                return RedirectToAction("Index", "Gasto");
            }
            else
            {
                return View("Index");
            }

        }
    }
}

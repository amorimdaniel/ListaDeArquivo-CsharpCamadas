using Data;
using Filtros;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ListaDeArquivos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class GastoController : Controller
    {
        private readonly ISessao _sessao;
        private readonly IGastos _gastos;
        public GastoController(ISessao sessao, IGastos gastos)
        {
            _sessao = sessao;
            _gastos = gastos;
        }
        public IActionResult Index()
        {
            Usuario usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            var gasto = _gastos.ListarGastoPorUsuario(usuarioLogado.IdUsuario);
            return View(gasto);
        }
        public IActionResult Apagar(int Id)
        {
            _gastos.ExcluirGasto(Id);
            return RedirectToAction("Index");
        }
        public IActionResult Total()
        {
            Usuario usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            var total = _gastos.TotalGasto(usuarioLogado.IdUsuario);
            return View(total);
        }
    }
}

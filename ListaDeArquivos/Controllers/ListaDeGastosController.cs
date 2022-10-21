using Data;
using Filtros;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ListaDeArquivos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ListaDeGastosController : Controller
    {
        private readonly ISessao _sessao;
        private readonly IGastos _gastos;
        public ListaDeGastosController(ISessao sessao, IGastos gastos)
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
        public IActionResult Total()
        {
            Usuario usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            var total = _gastos.TotalGasto();
            return View(total);
        }
    }
}

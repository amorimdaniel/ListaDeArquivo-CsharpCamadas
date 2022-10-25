using Data;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ListaDeArquivos.Controllers
{
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
                if (_gastos.CadastrarGasto(gasto, usuarioLogado.IdUsuario) == true)
                {
                    TempData["Mensagem"] = $"CADASTRADO COM SUCESSO";
                    return RedirectToAction("Index", "Gastos");
                }
                else
                {
                    TempData["Mensagem"] = $"erro";
                }
            }
            return View("Index");
        }
    }
}

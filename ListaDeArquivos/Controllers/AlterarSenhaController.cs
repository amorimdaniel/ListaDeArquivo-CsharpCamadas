using Data;
using Filtros;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ListaDeArquivos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class AlterarSenhaController : Controller
    {
        private readonly ISessao _sessao;
        private readonly IUsuarios _usuarios;
        public AlterarSenhaController(ISessao sessao, IUsuarios usuarios)
        {
            _sessao = sessao;
            _usuarios = usuarios;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Alterar(string senha)
        {
            Usuario usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            if (_usuarios.AtualizarSenha(senha, usuarioLogado.Email) == true)
            {
                TempData["Mensagem"] = $"Senha Alterada! Faça login novamente";
                _sessao.RemoverSessaoUsuario();
                return RedirectToAction("Index", "Login");

            }
            TempData["Mensagem"] = $"E-mail Inválido";
            return View("Index");
        }
    }
}

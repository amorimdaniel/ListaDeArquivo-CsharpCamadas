using Data;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeArquivos.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISessao _sessao;
        private readonly IUsuarios _usuario;


        public LoginController(ISessao sessao, IUsuarios usuarios)
        {
            _sessao = sessao;
            _usuario = usuarios;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Arquivo");
            return View();
        }
        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Entrar(string email, string senha)
        {
            if (email != "" && senha != "")
            {
                Usuario user = _usuario.BuscarUsuario(email, senha);
                if (user != null)
                {
                    _sessao.CriarSessaoDoUsuario(user);
                    return RedirectToAction("Index", "Arquivo");
                }
                TempData["Mensagem"] = $"USUÁRIO OU SENHA INVÁLIDOS";
            }
            TempData["Mensagem"] = $"USUÁRIO OU SENHA INVÁLIDOS";
            return View("Index");
        }
    }
}

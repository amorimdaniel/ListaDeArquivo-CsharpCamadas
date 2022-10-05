using Data;
using Helper;
using ListaDeArquivos.Helper;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace ListaDeArquivos.Controllers
{
    public class RedefinirSenhaController : Controller
    {
        private readonly IUsuarios _usuario;
        private readonly IEmail _email;
        public RedefinirSenhaController(IUsuarios usuarios, IEmail email)
        {
            _usuario = usuarios;
            _email = email;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Redefinir(string email)
        {
            bool resposta = _usuario.BuscarEmail(email);
            if (resposta == true)
            {
                _email.Enviar(email);
                TempData["Mensagem"] = $"Verifique seu e-mail";
                return RedirectToAction("Index", "Login");
            }         
            TempData["Mensagem"] = $"E-mail Inválido";
            return View("Index");
        }
    }
}

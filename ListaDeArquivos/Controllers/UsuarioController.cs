using Data;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeArquivos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarios _usuario;

        public UsuarioController(IUsuarios usuarios)
        {
            _usuario = usuarios;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if(_usuario.CadastrarUsuario(usuario) == true)
                {
                    TempData["Mensagem"] = $"CADASTRADO COM SUCESSO";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    TempData["Mensagem"] = $"NOME  DE USUÁRIO OU EMAIL JÁ EXISTE";
                }
            }
            return View("Index");
        }
    }
}

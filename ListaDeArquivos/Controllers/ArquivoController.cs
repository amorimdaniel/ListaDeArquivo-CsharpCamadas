﻿using Data;
using Filtros;
using Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeArquivos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ArquivoController : Controller
    {
        private readonly ISessao _sessao;
        private readonly IArquivos _arquivos;
        public ArquivoController(ISessao sessao, IArquivos arquivos)
        {
            _sessao = sessao;
            _arquivos = arquivos;
        }

        public IActionResult Index()
        {
            Usuario usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            var arquivos = _arquivos.ListarArquivoPorUsuario(usuarioLogado.IdUsuario);
            return View(arquivos);
        }

        [HttpPost]
        public IActionResult SalvarImagem(IList<IFormFile> arquivo)
        {
            if(arquivo == null || arquivo.Count == 0)
            {
                TempData["Mensagem"] = $"Erro - Selecione um arquivo";
                return RedirectToAction("Index");
            }
            if (arquivo.Count != 1)
            {
                TempData["Mensagem"] = $"Erro - Selecione apenas um arquivo";
                return RedirectToAction("Index");
            }
            if(arquivo[0].Length >= 100000)
            {
                TempData["Mensagem"] = $"Erro - Tamanho do arquivo é muito grande";
                return RedirectToAction("Index");
            }

            Usuario usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            _arquivos.CadastrarArquivo(arquivo, usuarioLogado.IdUsuario);
            return RedirectToAction("Index");

        }
        public IActionResult Visualizar(int IdArquivo)
        {
            Arquivo arquivo = _arquivos.AbrirArquivo(IdArquivo);
            return File(arquivo.Dados, arquivo.DadosTipo);
        }
        public IActionResult Baixar(int IdArquivo)
        {
            Arquivo arquivo = _arquivos.AbrirArquivo(IdArquivo);
            return File(arquivo.Dados, arquivo.DadosTipo, arquivo.Nome);
        }

        public IActionResult Apagar(int Id)
        {
            _arquivos.ExcluirArquivo(Id);
            return RedirectToAction("Index");
        }
    }
}


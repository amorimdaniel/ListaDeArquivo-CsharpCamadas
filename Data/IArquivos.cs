using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IArquivos
    {
        string CadastrarArquivo(IList<IFormFile> arquivos, int iDUsuario);
        void ExcluirArquivo(int id);
        Arquivo AbrirArquivo(int IdArquivo);
        IEnumerable<Arquivo> ListarArquivoPorUsuario(int IdUsuario);
    }
}

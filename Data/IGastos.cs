using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IGastos
    {
        string CadastrarGasto(Gasto gasto, int IdUsuario);
        void ExcluirGasto(int id);
        decimal TotalGasto();
        IEnumerable<Gasto> ListarGastoPorUsuario(int IdUsuario);
    }
}

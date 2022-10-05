using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Arquivo
    {
        public int IdArquivo{ get; set; }
        public string Nome { get; set; }
        public byte[] Dados { get; set; }
        public string DadosTipo { get; set; }
        public int IdUsuario { get; set; }

    }

}

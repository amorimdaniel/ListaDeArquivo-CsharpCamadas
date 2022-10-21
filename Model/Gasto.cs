using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Gasto
    {
        public int IdGasto { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public decimal Preco { get; set; }
        public int IdUsuario { get; set; }
        public decimal Total { get; set; }

    }
}

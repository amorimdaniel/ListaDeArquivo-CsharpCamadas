using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Linq;

namespace Model
{
    public class Gasto
    {
        public int IdGasto { get; set; }

        [Required(ErrorMessage = "Descrição Necessário")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamanho do {0} precisa ser entre {2} e {1}")]
        public string Descricao { get; set; }
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "Preço Necessário")]
        [Range(1.0, 50000.0, ErrorMessage = "{0} deve ser entre {1} e {2}")]
        [DisplayFormat(DataFormatString = "{0:f2}")]
        public decimal Preco { get; set; }
        public int IdUsuario { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model
{
    public class UsuarioSemSenha
    {

        [EmailAddress(ErrorMessage = "E-mail informado inválido")]
        [Required(ErrorMessage = "Digite o e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }
}

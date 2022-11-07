using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Digite o nome")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Tamnho do {0} precisa ser entre {2} e {1}")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite a senha")]
        public string Senha { get; set; }

        [EmailAddress(ErrorMessage = "E-mail informado inválido")]
        [Required(ErrorMessage = "Digite o e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public Usuario()
        {
        }

        public Usuario(string senha, string email)
        {
            Senha = senha;
            Email = email;
        }
    }
}

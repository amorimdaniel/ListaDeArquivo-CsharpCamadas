using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Digite o nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite a senha")]
        public string Senha { get; set; }

        [EmailAddress(ErrorMessage = "E-mail informado inválido")]
        [Required(ErrorMessage = "Digite o e-mail")]
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

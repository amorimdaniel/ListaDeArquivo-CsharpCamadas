using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IUsuarios
    {
        bool CadastrarUsuario(Usuario usuario);
        void ExcluirUsuario(int id);
        Usuario BuscarUsuario(string email, string senha);
        bool AtualizarSenha(string novaSenha, string email);
        bool BuscarEmail(string email);
    }
}

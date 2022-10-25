using Model;
using System;
using System.Data.SqlClient;

namespace Data
{
    public class Usuarios : IUsuarios
    {
        readonly Conexao conexao = new Conexao();
        readonly SqlCommand cmd = new SqlCommand();
        public bool CadastrarUsuario(Usuario usuario)
        {
            using (cmd.Connection = conexao.conectar())
            {
                var nome = usuario.Nome;
                var email = usuario.Email;
                Usuario user = new Usuario();

                cmd.CommandText = @"select * from Usuario where Nome=@nome or Email=@email";
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.ExecuteNonQuery();

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        user.IdUsuario = (int)dataReader["IdUsuario"];
                        user.Nome = (string)dataReader["Nome"];
                        user.Senha = (string)dataReader["Senha"];
                        user.Email = (string)dataReader["Email"];
                    }
                }
                if (user.Nome == usuario.Nome || user.Email == usuario.Email)
                {
                    return false;
                }
                else
                {
                    cmd.CommandText = @"insert into Usuario (Nome, Senha, Email) values (@Nomes, @Senhas, @Emails)";
                    cmd.Parameters.AddTipado("@Nomes", System.Data.SqlDbType.VarChar, usuario.Nome);
                    cmd.Parameters.AddTipado("@Senhas", System.Data.SqlDbType.VarChar, usuario.Senha);
                    cmd.Parameters.AddTipado("@Emails", System.Data.SqlDbType.VarChar, usuario.Email);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }
        public void ExcluirUsuario(int id)
        {
            using (cmd.Connection = conexao.conectar())
            {
                cmd.CommandText = @"delete from Usuario where id=@id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conexao.desconectar();
            }
        }

        public Usuario BuscarUsuario(string email, string senha)
        {
            using (cmd.Connection = conexao.conectar())
            {
                string Email = email;
                Usuario user = new Usuario();
                cmd.CommandText = @"select * from Usuario where Email=@Email";
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.ExecuteNonQuery();

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        user.IdUsuario = (int)dataReader["IdUsuario"];
                        user.Nome = (string)dataReader["Nome"];
                        user.Senha = (string)dataReader["Senha"];
                        user.Email = (string)dataReader["Email"];
                    }
                }
                if (email != user.Email || senha != user.Senha)
                {
                    return null;
                }
                return user;
            }
        }

        public bool AtualizarSenha(string novaSenha, string mail)
        {
            using (cmd.Connection = conexao.conectar())
            {
                string senha = novaSenha;
                string email = mail;
                cmd.CommandText = @"update Usuario set Senha=@senha where Email=@email";
                cmd.Parameters.AddWithValue("@Senha", senha);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        public bool BuscarEmail(string email)
        {
            using (cmd.Connection = conexao.conectar())
            {
                string mail = email;
                Usuario user = new Usuario();
                cmd.CommandText = @"select * from Usuario where Email=@email";
                cmd.Parameters.AddWithValue("@Email", mail);
                cmd.ExecuteNonQuery();

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        user.IdUsuario = (int)dataReader["IdUsuario"];
                        user.Nome = (string)dataReader["Nome"];
                        user.Senha = (string)dataReader["Senha"];
                        user.Email = (string)dataReader["Email"];
                    }
                }
                if (user.Email == email)
                {
                    return true;
                }
                return false;
            }
        }
    }
}



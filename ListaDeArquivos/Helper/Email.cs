using Data;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;

namespace ListaDeArquivos.Helper
{
    public class Email : IEmail
    {

        readonly Conexao conexao = new Conexao();
        readonly SqlCommand cmd = new SqlCommand();

        public bool Enviar(string email)
        {
            Random aleatorio = new Random();
            string Senha = Convert.ToString(aleatorio.Next(1000,2000));

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress("dan-amorim@live.com", "Sistema de documentos")
            };

            mail.To.Add(email);
            mail.Subject = "Sistema de documentos - Nova senha";
            mail.Body = "Sua nova senha: " + Senha;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587);
                
            smtp.Credentials = new NetworkCredential("dan-amorim@live.com", "0920141340Dan");
            smtp.EnableSsl = true;
            smtp.Send(mail);

            using (cmd.Connection = conexao.conectar())
            {
                cmd.CommandText = @"update Usuario set Senha=@Senha where Email=@email";
                cmd.Parameters.AddWithValue("@Senha", Senha);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.ExecuteNonQuery();
            }
            return true;
        }
    }
}

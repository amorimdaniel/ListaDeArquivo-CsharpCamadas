using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Data
{
    public class Arquivos : IArquivos
    {
        readonly Conexao conexao = new Conexao();
        readonly SqlCommand cmd = new SqlCommand();
        Arquivo arquivo;
        public void CadastrarArquivo(IList<IFormFile> arquivos, int iDUsuario)
        {
            IFormFile arquivoCarregado = arquivos.FirstOrDefault();
            if (arquivoCarregado != null)
            {
                MemoryStream ms = new MemoryStream();
                arquivoCarregado.OpenReadStream().CopyTo(ms);

                arquivo = new Arquivo
                {
                    Nome = arquivoCarregado.FileName,
                    Dados = ms.ToArray(),
                    DadosTipo = arquivoCarregado.ContentType,
                    IdUsuario = iDUsuario
                };

                using (cmd.Connection = conexao.conectar())
                {
                    cmd.CommandText = @"insert into Arquivo (Nome, Dados, DadosTipo, IdUsuario) values (@Nome, @Dados, @DadosTipo, @iDUsuario)";
                    cmd.Parameters.AddTipado("@Nome", System.Data.SqlDbType.VarChar, arquivo.Nome);
                    cmd.Parameters.AddTipado("@Dados", System.Data.SqlDbType.VarBinary, arquivo.Dados);
                    cmd.Parameters.AddTipado("@DadosTipo", System.Data.SqlDbType.VarChar, arquivo.DadosTipo);
                    cmd.Parameters.AddTipado("@IdUsuario", System.Data.SqlDbType.Int, arquivo.IdUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ExcluirArquivo(int id)
        {
            using (cmd.Connection = conexao.conectar())
            {
                cmd.CommandText = @"delete from Arquivo where IdArquivo=@IdArquivo";
                cmd.Parameters.AddWithValue("@IdArquivo", id);
                cmd.ExecuteNonQuery();
            }
        }

        public Arquivo AbrirArquivo(int IdArquivo)
        {
            Arquivo arquivo = new Arquivo();
            using (cmd.Connection = conexao.conectar())
            {
                cmd.CommandText = @"select * from Arquivo where IdArquivo=@IdArquivo";
                cmd.Parameters.AddWithValue("@IdArquivo", IdArquivo);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        arquivo.IdArquivo = (int)dataReader["IdArquivo"];
                        arquivo.Nome = (string)dataReader["Nome"];
                        arquivo.Dados = (byte[])dataReader["Dados"];
                        arquivo.DadosTipo = (string)dataReader["DadosTipo"];
                        arquivo.IdUsuario = (int)dataReader["IdUsuario"];
                    }
                }
                return arquivo;
            }
        }

        public IEnumerable<Arquivo> ListarArquivoPorUsuario(int IdUsuario)
        {
            List<Arquivo> ListaArquivo = new List<Arquivo>();

            var listAnterior = ListaArquivo as IDisposable;
            if (listAnterior != null)
            {
                listAnterior.Dispose();
            }

            using (cmd.Connection = conexao.conectar())
            {
                cmd.CommandText = @"select * from Arquivo where IdUsuario=@IdUsuario";
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        arquivo = new Arquivo();
                        arquivo.IdArquivo = (int)dataReader["IdArquivo"];
                        arquivo.Nome = (string)dataReader["Nome"];
                        arquivo.Dados = (byte[])dataReader["Dados"];
                        arquivo.DadosTipo = (string)dataReader["DadosTipo"];
                        arquivo.IdUsuario = (int)dataReader["IdUsuario"];
                        ListaArquivo.Add(arquivo);
                    }
                }
                return ListaArquivo;
            }
        }
    }
}
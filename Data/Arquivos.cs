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
        public string CadastrarArquivo(IList<IFormFile> arquivo, int iDUsuario)
        {
            Arquivo arqui;

            if (arquivo == null || arquivo.Count == 0)
            {
                return "Erro - Selecione um arquivo";

            }
            if (arquivo.Count != 1)
            {
                return "Erro - Selecione apenas um arquivo";

            }
            if (arquivo[0].Length >= 100000)
            {
                return "Erro - Tamanho do arquivo é muito grande";
            }

            IFormFile arquivoCarregado = arquivo.FirstOrDefault();
            if (arquivoCarregado != null)
            {
                MemoryStream ms = new MemoryStream();
                arquivoCarregado.OpenReadStream().CopyTo(ms);

                arqui = new Arquivo
                {
                    Nome = arquivoCarregado.FileName,
                    Dados = ms.ToArray(),
                    DadosTipo = arquivoCarregado.ContentType,
                    IdUsuario = iDUsuario
                };

                using (cmd.Connection = conexao.conectar())
                {
                    cmd.CommandText = @"insert into Arquivo (Nome, Dados, DadosTipo, IdUsuario) values (@Nome, @Dados, @DadosTipo, @iDUsuario)";
                    cmd.Parameters.AddTipado("@Nome", System.Data.SqlDbType.VarChar, arqui.Nome);
                    cmd.Parameters.AddTipado("@Dados", System.Data.SqlDbType.VarBinary, arqui.Dados);
                    cmd.Parameters.AddTipado("@DadosTipo", System.Data.SqlDbType.VarChar, arqui.DadosTipo);
                    cmd.Parameters.AddTipado("@IdUsuario", System.Data.SqlDbType.Int, arqui.IdUsuario);
                    cmd.ExecuteNonQuery();
                }
            }
            return "Enviado com sucesso";
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
            Arquivo arquivo;

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
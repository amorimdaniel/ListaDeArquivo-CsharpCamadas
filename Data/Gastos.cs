using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Data
{
    public class Gastos : IGastos
    {
        readonly Conexao conexao = new Conexao();
        readonly SqlCommand cmd = new SqlCommand();
        public string CadastrarGasto(Gasto gasto, int IdUsuario)
        {
            if (gasto.Descricao != null && IdUsuario != 0)
            {

                gasto = new Gasto
                {
                    Descricao = gasto.Descricao,
                    Data = DateTime.Now,
                    Preco = gasto.Preco,
                    IdUsuario = IdUsuario,
                };

                using (cmd.Connection = conexao.conectar())
                {
                    cmd.CommandText = @"insert into Gasto (Descricao, Data, Preco, IdUsuario) values (@Descricao, @Data, @Preco, @iDUsuario)";
                    cmd.Parameters.AddTipado("@Descricao", System.Data.SqlDbType.VarChar, gasto.Descricao);
                    cmd.Parameters.AddTipado("@Data", System.Data.SqlDbType.DateTime, gasto.Data);
                    cmd.Parameters.AddTipado("@Preco", System.Data.SqlDbType.Decimal, gasto.Preco);
                    cmd.Parameters.AddTipado("@IdUsuario", System.Data.SqlDbType.Int, IdUsuario);
                    cmd.ExecuteNonQuery();
                }
                return "Cadastrado com sucesso";
            }
            return "Erro";
        }

        public void ExcluirGasto(int id)
        {
            using (cmd.Connection = conexao.conectar())
            {
                cmd.CommandText = @"delete from Gasto where IdGasto=@IdGasto";
                cmd.Parameters.AddWithValue("@IdGasto", id);
                cmd.ExecuteNonQuery();
            }
        }

        public IEnumerable<Gasto> ListarGastoPorUsuario(int IdUsuario)
        {
            Gasto gastos;

            List<Gasto> ListaGastos = new List<Gasto>();

            var listAnterior = ListaGastos as IDisposable;
            if (listAnterior != null)
            {
                listAnterior.Dispose();
            }

            using (cmd.Connection = conexao.conectar())
            {
                cmd.CommandText = @"select * from Gasto where IdUsuario=@IdUsuario";
                cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        gastos = new Gasto
                        {
                            IdGasto = (int)dataReader["IdGasto"],
                            Descricao = (string)dataReader["Descricao"],
                            Data = (DateTime)dataReader["Data"],
                            Preco = (decimal)dataReader["Preco"],
                            IdUsuario = (int)dataReader["IdUsuario"]
                        };
                        ListaGastos.Add(gastos);
                    }
                }
                return ListaGastos;
            }
        }

        public decimal TotalGasto()
        {
            using (cmd.Connection = conexao.conectar())
            {
                cmd.CommandText = @"select sum(preco) from gasto";
                cmd.ExecuteNonQuery();
                decimal total = 0; 
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        total = (decimal)dataReader["Preco"];
                        total += total;
                    }
                }
                return total;
            }
        }
    }
}

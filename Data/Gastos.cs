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
        Gasto gastos;
        public bool CadastrarGasto(Gasto gasto, int IdUsuario)
        {
            if (gasto != null)
            {

                gasto = new Gasto
                {
                    Descricao = gasto.Descricao,
                    Data = gasto.Data,
                    Preco = gasto.Preco,
                    IdUsuario = IdUsuario,
                };

                using (cmd.Connection = conexao.conectar())
                {
                    cmd.CommandText = @"insert into Gasto (Descricao, Data, Preco, IdUsuario) values (@Descricao, @Data, @Preco, @iDUsuario)";
                    cmd.Parameters.AddTipado("@Descricao", System.Data.SqlDbType.VarChar, gasto.Descricao);
                    cmd.Parameters.AddTipado("@Data", System.Data.SqlDbType.DateTime, gasto.Data);
                    cmd.Parameters.AddTipado("@Preco", System.Data.SqlDbType.SmallMoney, gasto.Preco);
                    cmd.Parameters.AddTipado("@IdUsuario", System.Data.SqlDbType.Int, IdUsuario);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            return false;
        }

        public void ExcluirGasto(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gasto> ListarGastoPorUsuario(int IdUsuario)
        {
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
                        gastos = new Gasto();
                        gastos.IdGasto = (int)dataReader["IdGasto"];
                        gastos.Descricao = (string)dataReader["Descricao"];
                        gastos.Data = (DateTime)dataReader["Data"];
                        gastos.Preco = (decimal)dataReader["Preco"];
                        gastos.IdUsuario = (int)dataReader["IdUsuario"];
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

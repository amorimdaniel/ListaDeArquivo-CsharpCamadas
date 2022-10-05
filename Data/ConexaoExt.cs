using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Data
{
    public static class ConexaoExt
    {
        /// <summary>
        /// <para>Adiciona um SqlParameter já tipado na coleção do SqlCommand</para>
        /// <para>Essa extensão é necessária para diminuir o tempo de processamento</para>
        /// <para>Em queries como NVarchar e Varchar.</para>
        /// </summary>
        /// <param name="paramCollection">SqlParameterCollection</param>
        /// <param name="Nome">Nome do parametro (como definido na query)</param>
        /// <param name="Tipo">Tipo de dado no SQL Server.</param>
        /// <param name="Tamanho">Tamanho do campo.</param>
        /// <param name="Valor">Valor do parametro.</param>
        /// <returns>O parametro criado.</returns>
        public static SqlParameter AddTipado(this SqlParameterCollection paramCollection, string Nome, SqlDbType Tipo, int Tamanho, object Valor)
        {
            var parametro = new SqlParameter
            {
                ParameterName = Nome,
                SqlDbType = Tipo,
                Size = Tamanho,
                Value = Valor ?? DBNull.Value
            };
            paramCollection.Add(parametro);
            return parametro;
        }
        /// <summary>
        /// <para>Adiciona um SqlParameter já tipado na coleção do SqlCommand</para>
        /// <para>Essa extensão é necessária para diminuir o tempo de processamento</para>
        /// <para>Em queries como NVarchar e Varchar.</para>
        /// </summary>
        /// <param name="paramCollection">SqlParameterCollection</param>
        /// <param name="Nome">Nome do parametro (como definido na query).</param>
        /// <param name="Tipo">Tipo de dado no SQL Server.</param>
        /// <param name="Valor">Valor do parametro.</param>
        /// <returns>O parametro criado.</returns>
        public static SqlParameter AddTipado(this SqlParameterCollection paramCollection, string Nome, SqlDbType Tipo, object Valor)
        {
            var parametro = new SqlParameter
            {
                ParameterName = Nome,
                SqlDbType = Tipo,
                Value = Valor ?? DBNull.Value
            };
            paramCollection.Add(parametro);
            return parametro;
        }
    }
}

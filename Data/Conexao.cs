using System;
using System.Data.SqlClient;

namespace Data
{
    public class Conexao
    {
        SqlConnection con = new SqlConnection();
        public Conexao()
        {
            con.ConnectionString = @"Data Source=LAPTOP-1A83C5S1\SQLSERVER;Initial Catalog=DB_Arquivos;Integrated Security=True";
        }

        public SqlConnection conectar(){
            if(con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public SqlConnection desconectar()
        {
            if(con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
            return con;
        }
    }
}

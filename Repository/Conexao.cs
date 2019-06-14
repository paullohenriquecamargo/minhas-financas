using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Conexao
    {
        string CadeiadeConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\65982\Documents\Financeiro.mdf;Integrated Security=True;Connect Timeout=30";

        public SqlCommand Conectar()
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = CadeiadeConexao;
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            return comando;
        }
    }
}

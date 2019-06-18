using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repository
{
    public class ContasPagarRepository : IRepositoryContaPagar
    {
        private Conexao conexao;
        
        public ContasPagarRepository()
        {
            conexao = new Conexao();
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"DELETE FROM contaspagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;
        }

        public bool Atualizar(ContaPagar contaPagar)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"UPDATE contaspagar SET 
nome = @NOME,
valor = @VALOR,
tipo = @TIPO,
descricao = @DESCRICAO,
estatus = @ESTATUS
WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            comando.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            comando.Parameters.AddWithValue("@TIPO", contaPagar.Tipo);
            comando.Parameters.AddWithValue("@DESCRICAO", contaPagar.Descricao);
            comando.Parameters.AddWithValue("@ESTATUS", contaPagar.Estatus);
            comando.Parameters.AddWithValue("@ID", contaPagar.Id);
            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;
            
        }

        public int Inserir(ContaPagar contaPagar)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"INSERT INTO contaspagar (nome, valor, tipo, descricao, estatus)
OUTPUT INSERTED.ID
VALUES (@NOME, @VALOR, @TIPO, @DESCRICAO, @ESTATUS)";
            comando.Parameters.AddWithValue("@NOME", contaPagar.Nome);
            comando.Parameters.AddWithValue("@VALOR", contaPagar.Valor);
            comando.Parameters.AddWithValue("@TIPO", contaPagar.Tipo);
            comando.Parameters.AddWithValue("@DESCRICAO", contaPagar.Descricao);
            comando.Parameters.AddWithValue("@ESTATUS", contaPagar.Estatus);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;                        
        }

        public ContaPagar ObterPeloId(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"SELECT * FROM contaspagar WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if (tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            ContaPagar contaPagar = new ContaPagar();
            contaPagar.Id = Convert.ToInt32(linha["id"]);
            contaPagar.Nome = linha["nome"].ToString();
            contaPagar.Valor = Convert.ToDecimal(linha["valor"]);
            contaPagar.Tipo = linha["tipo"].ToString();
            contaPagar.Descricao = linha["descricao"].ToString();
            contaPagar.Estatus = linha["estatus"].ToString();

            return contaPagar;
        }

        public List<ContaPagar> ObterTodos(string busca)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"SELECT * FROM contaspagar WHERE nome LIKE @NOME";

            busca = $"%{busca}%";
            comando.Parameters.AddWithValue("@NOME", busca);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<ContaPagar> contasPagar = new List<ContaPagar>();
            for(int i =0; i <tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                ContaPagar contaPagar = new ContaPagar();
                contaPagar.Id = Convert.ToInt32(linha["id"]);
                contaPagar.Nome = linha["nome"].ToString();
                contaPagar.Valor = Convert.ToDecimal(linha["valor"]);
                contaPagar.Tipo = linha["tipo"].ToString();
                contaPagar.Descricao = linha["descricao"].ToString();
                contaPagar.Estatus = linha["estatus"].ToString();
                contasPagar.Add(contaPagar);
            }
            return contasPagar;                
         }
    }
}

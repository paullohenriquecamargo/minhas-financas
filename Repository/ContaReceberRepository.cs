using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ContaReceberRepository : IRepositoryContaReceber
    {
        private Conexao conexao;
        public ContaReceberRepository()
        {
            conexao = new Conexao();
        }
        public bool Apagar(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"DELETE FROM contasreceber WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;
        }
        public bool Atualizar(ContaReceber contaReceber)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"UPDATE contareceber SET 
nome = @NOME,
valor = @VALOR,
tipo = @TIPO,
descricao = @DESCRICAO,
estatus = @ESTATUS
WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            comando.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            comando.Parameters.AddWithValue("@TIPO", contaReceber.Tipo);
            comando.Parameters.AddWithValue("@DESCRICAO", contaReceber.Descricao);
            comando.Parameters.AddWithValue("@ESTATUS", contaReceber.Estatus);
            int quantidadeafetada = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidadeafetada == 1;
        }
        public int Inserir(ContaReceber contaReceber)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"INSERT INTO (nome, valor, tipo, descricao, estatus)
OUTPUT INSERTED.ID
VALUES (@NOME, @VALOR, @TIPO, @DESCRICAO, @ESTATUS)";
            comando.Parameters.AddWithValue("@NOME", contaReceber.Nome);
            comando.Parameters.AddWithValue("@VALOR", contaReceber.Valor);
            comando.Parameters.AddWithValue("@TIPO", contaReceber.Tipo);
            comando.Parameters.AddWithValue("@DESCRICAO", contaReceber.Descricao);
            comando.Parameters.AddWithValue("@ESTATUS", contaReceber.Estatus);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;                
        }
        public ContaReceber ObterPeloId(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM contasreceber WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if(tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            ContaReceber contaReceber = new ContaReceber();
            contaReceber.Id = Convert.ToInt32(linha["id"]);
            contaReceber.Nome = linha["nome"].ToString();
            contaReceber.Valor = Convert.ToDecimal(linha["valor"]);
            contaReceber.Tipo = linha["tipo"].ToString();
            contaReceber.Descricao = linha["descricao"].ToString();
            contaReceber.Estatus = linha["estatus"].ToString();

            return contaReceber;
        }
        public List<ContaReceber> ObterTodos(string busca)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM contasreceber WHERE nme LIKE @NOME";

            busca = $"%{busca}%";
            comando.Parameters.AddWithValue("@NOME", busca);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<ContaReceber> contasReceber = new List<ContaReceber>();
            for(int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                ContaReceber contaReceber = new ContaReceber();
                contaReceber.Id = Convert.ToInt32(linha["id"]);
                contaReceber.Nome = linha["nome"].ToString();
                contaReceber.Valor = Convert.ToDecimal(linha["valor"]);
                contaReceber.Tipo = linha["tipo"].ToString();
                contaReceber.Descricao = linha["descricao"].ToString();
                contaReceber.Estatus = linha["estatus"].ToString();
                contasReceber.Add(contaReceber);
            }
            return contasReceber;
        }

    }
}

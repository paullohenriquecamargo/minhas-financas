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
    public class ClientePFRepository : IRepositoryClientePF
    {
        private Conexao conexao;

        public ClientePFRepository()
        {
            conexao = new Conexao();
        }

        public bool Apagar(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "DELETE FROM clientespf WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidadeafetada = Convert.ToInt32(comando.ExecuteNonQuery());
            comando.Connection.Close();
            return quantidadeafetada == 1;
        }

        public bool Atualizar(ClientePF clientesPf)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"UPDATE clientespf SET 
nome = @NOME,
cpf = @CPF,
data_nascimento = @DATA_NASCIMENTO,
rg = @RG
WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", clientesPf.Nome);
            comando.Parameters.AddWithValue("@CPF", clientesPf.Cpf);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", clientesPf.DataNascimento);
            comando.Parameters.AddWithValue("@RG", clientesPf.Rg);
            comando.Parameters.AddWithValue("@ID", clientesPf.Id);
            int quantidadeafetada = Convert.ToInt32(comando.ExecuteNonQuery());
            comando.Connection.Close();

            return quantidadeafetada == 1;
        }

        public int Inserir(ClientePF clientePF)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"INSERT INTO clientespf (nome, cpf, data_nascimento, rg)
OUTPUT INSERTED.ID
(@NOME, @CPF, @DATA_NASCIMENTO, @RG)";
            comando.Parameters.AddWithValue("@NOME", clientePF.Nome);
            comando.Parameters.AddWithValue("@CPF", clientePF.Cpf);
            comando.Parameters.AddWithValue("@DATA_NASCIMENTO", clientePF.DataNascimento);
            comando.Parameters.AddWithValue("@RG", clientePF.Rg);
            int id = Convert.ToInt32(comando.ExecuteNonQuery());
            comando.Connection.Close();
            return id;
        }

        public ClientePF ObterPeloId(int id)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM clientespf WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            if(tabela.Rows.Count == 0)
            {
                return null;
            }
            DataRow linha = tabela.Rows[0];
            ClientePF clientePF = new ClientePF();
            clientePF.Id = Convert.ToInt32(linha["id"]);
            clientePF.Nome = linha["nome"].ToString();
            clientePF.Cpf = linha["cpf"].ToString();
            clientePF.DataNascimento = Convert.ToDateTime(linha["data_nascimento"]);
            clientePF.Rg = linha["rg"].ToString();

            return clientePF;
        }
           
        public List<ClientePF> ObterTodos(string busca)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = "SELECT * FROM clientespf WHERE nome LIKE @NOME ";

            busca = $"%{busca}%";
            comando.Parameters.AddWithValue("@NOME", busca);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<ClientePF> clientesPF = new List<ClientePF>();
            for(int i= 0; i <tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[0];
                ClientePF clientePF = new ClientePF();
                clientePF.Id = Convert.ToInt32(linha["id"]);
                clientePF.Nome = linha["nome"].ToString();
                clientePF.Cpf = linha["cpf"].ToString();
                clientePF.DataNascimento = Convert.ToDateTime(linha["data_nascimento"]);
                clientePF.Rg = linha["rg"].ToString();
                clientesPF.Add(clientePF);
            }
            return clientesPF;
        }
    }
}

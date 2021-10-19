using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;


namespace sVenda.pBancoDeDados
{
    public class cBancoMySql : IAcessoBanco
    {
        private const string sConexao = "server=127.0.0.1;user id=root;password=poo150;database=projeto_vendas"; //user do sql
        public System.Data.DataSet ListarDados(string query)
        {
            MySqlConnection conexao = new MySqlConnection(sConexao);
            try
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, conexao);

                DataSet dados = new DataSet();
                dados.Clear();

                dataAdapter.Fill(dados);
                conexao.Open();

                return dados;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao executar instrução SQL: " + ex.Message);
            }
            finally { conexao.Close(); }

        }
        public void ExecutarComando(string query)
        {
            MySqlConnection conexao = new MySqlConnection(sConexao);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conexao);

                conexao.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao executar comando no banco de dados. " + ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}

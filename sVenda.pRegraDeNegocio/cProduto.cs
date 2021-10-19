using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using sVenda.pBancoDeDados;
using sVenda.pRegraDeNegocio;

namespace sVenda
{
    public class cProduto : cProdutoBase
    {
        public cProduto(string xCodigo, string xNome, double xValor)
        {
            Codigo = xCodigo;
            Nome = xNome;
            ValorUnitario = xValor;
        }

        public cProduto()
        {

        }

        public void Incluir()
        {
            try
            {
                string sql = @"INSERT 
                                INTO produto(codigo, nome, valor)
                                VALUES('" + Codigo + "','" + Nome + "'," + ValorUnitario.ToString() + ")";

                IAcessoBanco AcessoBanco = new cBancoMySql();

                AcessoBanco.ExecutarComando(sql);

            }
            catch (Exception ex) { throw ex; }
        }
        public void Alterar()
        {
            try
            {
                string sql = @"UPDATE produto
                                    set nome = '" + Nome + "', " +
                                    " valor = " + ValorUnitario.ToString() +
                                    " Where codigo = '" + Codigo + "'";

                IAcessoBanco AcessoBanco = new cBancoMySql();

                AcessoBanco.ExecutarComando(sql);
            }
            catch (Exception ex) { throw ex; }
        }
        public void Excluir()
        {
            try
            {
                string sql = @"delete 
                               from produto
                               Where codigo = '" + Codigo + "'";

                IAcessoBanco AcessoBanco = new cBancoMySql();
                AcessoBanco.ExecutarComando(sql);
            }
            catch (Exception ex) { throw ex; }
        }
        public List<cProduto> Listar()
        {
            try
            {
                string sql = @"SELECT * FROM produto";

                DataSet ds = new DataSet();

                IAcessoBanco AcessoBanco = new cBancoMySql();

                DataSet dados = AcessoBanco.ListarDados(sql);

                List<cProduto> ListaProduto = new List<cProduto>();

                foreach (DataRow linha in dados.Tables[0].Rows)
                {
                    cProduto objProduto = new cProduto()
                    {
                        Codigo = linha["codigo"].ToString(),
                        Nome = linha["nome"].ToString(),
                        ValorUnitario =  Convert.ToInt32(linha["valor"].ToString()),
                    };
                    ListaProduto.Add(objProduto);
                }

                return ListaProduto;
            }
            catch (Exception ex) { throw ex; }

        }
    }
}
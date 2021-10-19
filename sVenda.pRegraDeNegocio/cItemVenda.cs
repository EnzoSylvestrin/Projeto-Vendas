using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using sVenda.pBancoDeDados;
using System.Data;

namespace sVenda.pRegraDeNegocio
{
    public class cItemVenda : cProdutoBase 
    {
        string SQL;

        public int Quantidade { get; set; }

        public List<cItemVenda> itemvenda(string Codigo)
        {
            SQL = @"SELECT * from itemvenda where codigoProduto = '" + Codigo + "'";

            IAcessoBanco acessoBanco = new cBancoMySql();

            acessoBanco.ExecutarComando(SQL);

            DataSet dados = acessoBanco.ListarDados(SQL);

            List<cItemVenda> venda2 = new List<cItemVenda>();

            foreach (DataRow i in dados.Tables[0].Rows)
            {
                cItemVenda itemvenda = new cItemVenda();
                itemvenda.Quantidade = Convert.ToInt32(i["Quantidade"].ToString());
                itemvenda.ValorUnitario = Convert.ToDouble(i["Valor"]);
                itemvenda.Codigo = Convert.ToString(i["codigoProduto"]);

                venda2.Add(itemvenda);
            }
            return venda2;
                
        }
        public bool VendaRegistrada(string Codigo)
        {
            itemvenda(Codigo);
            List<cItemVenda> venda = this.itemvenda(Codigo);
            if (venda.Count > 0)
                return true;
            else
                return false;
        }
        public void Incluir(string CodigoVenda)
        {
            try
            {
                SQL = @"INSERT INTO itemvenda (codigoVenda, codigoProduto, quantidade, valor)
                                VALUES('" + CodigoVenda + "', '" + Codigo + "', " + Quantidade + ", " 
                                + ValorUnitario.ToString() + ")";

                IAcessoBanco acessoBanco = new cBancoMySql();
                acessoBanco.ExecutarComando(SQL);
            }
            catch (Exception ex) 
            {
                throw new Exception("Houve problema ao Incluir os dados." + ex.Message);
            }
        }
        public List<cItemVenda> Listar()
        {
            SQL = @"SELECT itemvenda.*, Produto.Nome
            FROM itemvenda, Produto WHERE itemvenda.codigoProduto = Produto.Codigo";
            
            IAcessoBanco acessoBanco = new cBancoMySql();

            DataSet dados = acessoBanco.ListarDados(SQL);

            List<cItemVenda> venda = new List<cItemVenda>();

            foreach (DataRow i in dados.Tables[0].Rows)
            {
                cItemVenda itemvenda = new cItemVenda();
                itemvenda.Quantidade = Convert.ToInt32(i["Quantidade"].ToString());
                itemvenda.Nome = i["Nome"].ToString();
                itemvenda.ValorUnitario = Convert.ToDouble(i["Valor"]);
                itemvenda.Codigo = Convert.ToString(i["codigoProduto"]);

                venda.Add(itemvenda);
            }
            return venda;
        }
        public List<cItemVenda> Listar(string CodigoProduto)
        {
            string SQL;

            SQL = @"SELECT itemvenda.*, Produto.Nome
            FROM itemvenda, Produto WHERE itemvenda.codigoProduto = Produto.Codigo AND itemvenda.codigoProduto = '" + CodigoProduto + "'"; 

            IAcessoBanco acessoBanco = new cBancoMySql();

            DataSet dados = acessoBanco.ListarDados(SQL);

            List<cItemVenda> venda = new List<cItemVenda>();

            foreach (DataRow i in dados.Tables[0].Rows)
            {

                cItemVenda itemvenda = new cItemVenda();
                itemvenda.Codigo = Convert.ToString(i["codigoProduto"]);
                itemvenda.Nome = i["Nome"].ToString();
                itemvenda.Quantidade = Convert.ToInt32(i["Quantidade"].ToString());
                itemvenda.ValorUnitario = Convert.ToDouble(i["Valor"]);

                venda.Add(itemvenda); 
            }
            return venda;

        }
    }
}

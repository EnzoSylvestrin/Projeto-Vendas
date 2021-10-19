using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using sVenda.pBancoDeDados;

namespace sVenda.pRegraDeNegocio
{
    public class cVenda
    {
        public string CodigoVenda {get; set;}
        public DateTime DataVenda {get; set;}
        public List<cItemVenda> ItensDaVenda {get; set;}

        public void IncluirVenda()
        {
            try
            {
                string sql = @"INSERT 
                                INTO Venda(codigoVenda, dataVenda)
                                VALUES('" + CodigoVenda + "', STR_TO_DATE('" + DataVenda.Day.ToString() + ", " + DataVenda.Month.ToString() + ", " + DataVenda.Year.ToString() + "', '%d,%m,%Y'))";

                IAcessoBanco AcessoBanco = new cBancoMySql();

                AcessoBanco.ExecutarComando(sql);

                foreach (cItemVenda i in ItensDaVenda)
                {
                    i.Incluir(CodigoVenda);
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}

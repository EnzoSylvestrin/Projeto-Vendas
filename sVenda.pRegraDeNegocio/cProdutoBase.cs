using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace sVenda.pRegraDeNegocio
{
    public class cProdutoBase
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public double ValorUnitario { get; set; }
    }
}

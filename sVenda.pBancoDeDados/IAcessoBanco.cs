using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace sVenda.pBancoDeDados
{
    public interface IAcessoBanco
    {
        DataSet ListarDados(string query);
        void ExecutarComando(string query);
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Persistencia
{
    class ContaDAL
    {
        private SqlConnection conn;
        private CategoriaDAL categoria;

        public ContaDAL(SqlConnection conn)
        {
            this.conn = conn;

            string stringConnection = Db.Conexao.GetStringConnection();

            this.categoria = new CategoriaDAL(new SqlConnection(stringConnection));
        }
    }
}

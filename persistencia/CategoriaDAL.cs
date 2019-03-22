using System;
using System.Data.SqlClient;

namespace Persistencia
{
    public class CategoriaDAL
    {
        private SqlConnection conn;

        public CategoriaDAL(SqlConnection conn)
        {
            this.conn = conn;
        }
    }
}

using System;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia
{
    public class CategoriaDAL
    {
        private SqlConnection conn;

        public CategoriaDAL(SqlConnection conn)
        {
            this.conn = conn;
        }

        public Categoria GetCategoria(int id)
        {
            Categoria categoria = new Categoria();
            var cmd = new SqlCommand("SELECT id, nome FROM dbo.categorias where id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();

            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    categoria.Id = Convert.ToInt32(rd["id"].ToString());
                    categoria.Nome = rd["nome"].ToString();
                }
                
                
            }

            conn.Close(); 

            return categoria;
        }
    }
}

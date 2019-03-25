using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Modelo;

namespace Persistencia
{
    public class CategoriaDAL
    {
        private SqlConnection conn;
        private CategoriaDAL categoria;

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

        public List<Categoria> ListarTodos()
        {
            List<Categoria> categorias = new List<Categoria>();

            var cmd = new SqlCommand("SELECT con.id, " +
                                     "con.descricao, " +
                                     "con.valor, " +
                                     "con.tipo, " +
                                     "con.data_vencimento, " +
                                     "cat.id as Categoria_ID, " +
                                     "cat.nome " +
                                     "FROM dbo.contas con " +
                                     "inner join dbo.categorias cat " +
                                     "on con.categorias_id = cat.id", conn);
            conn.Open();

            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    Categoria categoria = new Categoria()
                    {
                        Id = Convert.ToInt32(rd["id"].ToString()),
                        Nome = rd["nome"].ToString(),
                    };
                    
                    categorias.Add(categoria);
                }
            }

            conn.Close();

            return categorias;
        }
    }
}

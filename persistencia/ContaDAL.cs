using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Modelo;

namespace Persistencia
{
    public class ContaDAL
    {
        private SqlConnection conn;
        private CategoriaDAL categoria;

        public ContaDAL(SqlConnection conn)
        {
            this.conn = conn;

            string stringConnection = Db.Conexao.GetStringConnection();

            categoria = new CategoriaDAL(new SqlConnection(stringConnection));
        }

        public List<Conta> ListarTodos()
        {
            List<Conta> contas = new List<Conta>();

            var cmd = new SqlCommand("SELECT con.id, con.descricao, con.valor, con.tipo, con.data_vencimento, cat.id as Categoria_ID, cat.nome FROM dbo.conta con inner join dbo.categoria cat on con.categoria_id = cat.id", conn);
            conn.Open();

            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    Conta conta = new Conta()
                    {
                        Id = Convert.ToInt32(rd["id"].ToString()),
                        Descricao = rd["descricao"].ToString(),
                        Tipo = rd["tipo"].ToString(),
                        Valor = Convert.ToDouble(rd["valor"].ToString())
                    };

                    int id_categoria = Convert.ToInt32(rd["id"].ToString());
                    conta.Categoria = categoria.GetCategoria(id_categoria);

                    contas.Add(conta);
                }
            }

            return contas;

        }
    }
}

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
                    Conta conta = new Conta()
                    {
                        Id = Convert.ToInt32(rd["id"].ToString()),
                        Descricao = rd["descricao"].ToString(),
                        Tipo = Convert.ToChar(rd["tipo"].ToString()),
                        Valor = Convert.ToDouble(rd["valor"].ToString())
                    };

                    int id_categoria = Convert.ToInt32(rd["id"].ToString());
                    conta.Categoria = categoria.GetCategoria(id_categoria);

                    contas.Add(conta);
                }
            }

            return contas;

        }

        public void Salvar(Conta conta)
        {
            if(conta.Id == null)
            {
                Cadastrar(conta);
            }
            else
            {
                Editar(conta);
            }
        }

        void Cadastrar (Conta conta)
        {
            this.conn.Open();
            SqlCommand cmd = this.conn.CreateCommand();
            cmd.CommandText = "insert into contas(descricao, tipo, valor, data_vencimento, categorias_id) " +
                              "values (@descricao, @tipo, @valor, @data_vencimento, @categorias_id)";

            cmd.Parameters.AddWithValue("@descricao", conta.Descricao);
            cmd.Parameters.AddWithValue("@tipo", conta.Tipo);
            cmd.Parameters.AddWithValue("@valor", conta.Valor);
            cmd.Parameters.AddWithValue("@data_vencimento", conta.DataVencimento);
            cmd.Parameters.AddWithValue("@categorias_id", conta.Categoria.Id);

            cmd.ExecuteNonQuery();

            this.conn.Close();
        }

        void Editar(Conta conta)
        {

        }
    }
}

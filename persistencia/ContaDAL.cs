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

            StringBuilder sql = new StringBuilder("select ");
            sql.Append("con.id, con.descricao, con.valor, con.tipo, con.data_vencimento, cat.id as Categoria_ID ");
            sql.Append("from contas con ");
            sql.Append("inner join dbo.categorias cat ");
            sql.Append("on con.categorias_id = cat.id ");

            var cmd = new SqlCommand(sql.ToString(), conn);

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
                        Valor = Convert.ToDouble(rd["valor"].ToString()),
                        DataVencimento = Convert.ToDateTime(rd["data_vencimento"].ToString())
                    };

                    int id_categoria = Convert.ToInt32(rd["id"].ToString());
                    conta.Categoria = categoria.GetCategoria(id_categoria);

                    contas.Add(conta);
                }
            }
            conn.Close();

            return contas;
        }

        public List<Conta> ListarTodos(string data_inicial = "", string data_final = "")
        {
            List<Conta> contas = new List<Conta>();

            StringBuilder sql = new StringBuilder("select ");
            sql.Append("con.id, con.descricao, con.valor, con.tipo, con.data_vencimento, cat.id as Categoria_ID ");
            sql.Append("from contas con ");
            sql.Append("inner join dbo.categorias cat ");
            sql.Append("on con.categorias_id = cat.id ");


            if (!data_inicial.Equals("") && !data_final.Equals(""))
            {
                sql.Append("where con.data_vencimento between ");
                sql.Append("@data_inicial and @data_final ");
            }

            var cmd = new SqlCommand(sql.ToString(), conn);

            if (!data_inicial.Equals("") && !data_final.Equals(""))
            {
                cmd.Parameters.AddWithValue("@data_inicial", data_inicial);
                cmd.Parameters.AddWithValue("@data_final", data_final);
            }

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
                        Valor = Convert.ToDouble(rd["valor"].ToString()),
                        DataVencimento = Convert.ToDateTime(rd["data_vencimento"].ToString())
                    };

                    int id_categoria = Convert.ToInt32(rd["id"].ToString());
                    conta.Categoria = categoria.GetCategoria(id_categoria);

                    contas.Add(conta);
                }
            }
            conn.Close();

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

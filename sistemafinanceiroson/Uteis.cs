using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using Modelo;
using Persistencia;
using System.Data.SqlClient;

namespace SistemaFinanceiroSoN
{
    public class Uteis
    {
        private List<Conta> contas;
        private List<Categoria> categorias;

        private ContaDAL conta;
        private CategoriaDAL categoria;
        
        public Uteis()
        {
            string strConn = Db.Conexao.GetStringConnection();
            conta = new ContaDAL(new SqlConnection(strConn));
            categoria = new CategoriaDAL(new SqlConnection(strConn));
        }

        public static void MontaMenu()
        {
            MontaHearder("CONTROLE FINANCEIRO");
            WriteLine("Selecione uma Opção Abaixo:");
            WriteLine("------------------------------");

            WriteLine("1 - Listar");
            WriteLine("2 - Cadastrar");
            WriteLine("3 - Editar");
            WriteLine("4 - Excluir");
            WriteLine("5 - Relatorio");
            WriteLine("6 - Sair");


            WriteLine("Opção: ");
        }

        public static void MontaHearder(string titulo, char cod = '=', int len =30)
        {
            WriteLine(new string(cod, len) + " " + titulo + " " + new string(cod, len) + "\n");
            
        }

        public void Executar(int opc)
        {
            switch (opc)
            {
                case 1:
                    Title = "Listagem de Contas - CONTROLE FINANCEIRO";
                    MontaHearder("Listagem de Contas");

                    foreach (var c in conta.ListarTodos())
                    {
                        WriteLine("Descricao: " + c.Descricao);
                    }

                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
    }
}

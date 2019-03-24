using System;
using static System.Console;
using Modelo;
using System.Collections.Generic;
using Persistencia;
using System.Data.SqlClient;

namespace SistemaFinanceiroSoN
{
    class Program
    {
        private List<Conta> contas;
        private List<Categoria> categorias;

        private ContaDAL conta;
        private CategoriaDAL categoria;

        public Program()
        {
            string strConn = Db.Conexao.GetStringConnection();
            this.conta = new ContaDAL(new SqlConnection(strConn));
            this.categoria = new CategoriaDAL(new SqlConnection(strConn));
        }

        static void Main(string[] args)
        {
            Uteis uteis = new Uteis();

            int opc;
            do
            {
                Title = "CONTROLE FINANCEIRO";
                Uteis.MontaMenu();
                opc = Convert.ToInt32(ReadLine());

                if(!(opc >= 1 && opc <= 6))
                {
                    Clear();
                    BackgroundColor = ConsoleColor.Red;
                    ForegroundColor = ConsoleColor.White;
                    Uteis.MontaHearder("OPÇÃO INVÁLIDA", 'X', 35);
                    Uteis.MontaHearder("DIGITE UMA OPÇÃO CORRETA", 'X');
                    ResetColor();
                }
                else
                {
                    Clear();
                    
                    uteis.Executar(opc);
                }

            } while (opc != 6);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using Modelo;
using Persistencia;
using System.Data.SqlClient;
using ConsoleTables;

namespace SistemaFinanceiroSoN
{
    public class Uteis
    {
        private List<Conta> contas;
        private List<Categoria> categorias;

        private ContaDAL contaDal;
        private CategoriaDAL categoriaDal;
        
        public Uteis()
        {
            string strConn = Db.Conexao.GetStringConnection();
            contaDal = new ContaDAL(new SqlConnection(strConn));
            categoriaDal = new CategoriaDAL(new SqlConnection(strConn));
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


            Write("Opção: ");
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

                    ConsoleTable table = new ConsoleTable("Id", "Descrição","Tipo", "Valor");

                    foreach (var c in contaDal.ListarTodos())
                    {
                        table.AddRow(c.Id, c.Descricao, c.Tipo.Equals('R') ? "Receber" : "Pagar", String.Format("{0:c}", c.Valor));
                    }

                    table.Write();

                    ReadLine();
                    Clear();

                    break;
                case 2:
                    Title = "Nova Contas - CONTROLE FINANCEIRO";
                    MontaHearder("Cadastrar Nova Contas");

                    string descricao = "";

                    do
                    {
                        Write("Informe a descrição da Conta: ");
                        descricao = ReadLine();

                        if (descricao.Equals(""))
                        {
                            BackgroundColor = ConsoleColor.Red;
                            ForegroundColor = ConsoleColor.White;
                            Uteis.MontaHearder("Informe Uma Descrição",'*',28);
                            ResetColor();
                        }

                    } while (descricao.Equals(""));

                    Write("Informe o valor: ");
                    double valor = Convert.ToDouble(ReadLine());

                    WriteLine("informe o Tipo da Conta ('R' = Receber / 'P' = Pagar): ");
                    char tipo = Convert.ToChar(ReadLine());

                    Write("Informe data de Vencimento (dd/mm/aaaa): ");
                    DateTime dataVencimento = DateTime.Parse(ReadLine());

                    WriteLine("Selecione uma categoria pela ID: \n");
                    categoriaDal.ListarTodos();

                    table = new ConsoleTable("Id", "Nome");

                    foreach (var cat in categoriaDal.ListarTodos())
                    {
                        table.AddRow(cat.Id, cat.Nome);
                    }

                    table.Write();

                    Write("Categoria: ");
                    int catId = Convert.ToInt32(ReadLine());

                    Categoria categoria_cadastro = categoriaDal.GetCategoria(catId);

                    Conta conta = new Conta()
                    {
                        Descricao = descricao,
                        Valor = valor,
                        Tipo = tipo,
                        DataVencimento = dataVencimento,
                        Categoria = categoria_cadastro
                    };

                    contaDal.Salvar(conta);

                    BackgroundColor = ConsoleColor.DarkGreen;
                    ForegroundColor = ConsoleColor.White;
                    MontaHearder("SALVO COM SUCESSO", '+', 30);
                    ResetColor();

                    ReadLine();
                    Clear();

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

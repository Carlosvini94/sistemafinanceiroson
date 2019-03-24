using System;

namespace Db
{
    public class Conexao
    {
        //private static readonly string server = "RT-TI-18852";
        private static readonly string server = "DESKTOP-86IA8M0\\SQLEXPRESS2014";
        private static readonly string database = "SoN_Financeiro";
        private static readonly string username = "sa";
        //private static readonly string password = "Sistemas334815";
        private static readonly string password = "334815";

        public static string GetStringConnection() => $"Server={server};Database={database};User Id={username};Password={password}";
    }
}

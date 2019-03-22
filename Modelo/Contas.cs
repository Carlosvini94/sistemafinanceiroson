using System;
using System.Collections.Generic;
using System.Text;

namespace Modelo
{
    class Contas
    {
        private int? _id;
        private string _descricao;
        private char _tipo;
        private double _valor;
        private DateTime _data_vencimento;
        private Categoria _categoria;

        public int? Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }

        public char Tipo {
            get => _tipo;
            set => _tipo = !value.Equals('P') && !value.Equals('R') ? throw new Exception("Use P para pagar e R para receber") : value;
        }

        public DateTime DataVencimento { get; set; }
        public Categoria Categoria { get; set; }

    }
}

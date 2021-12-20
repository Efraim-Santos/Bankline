using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Models
{
    public class TransactionDefaultViewModel
    {
        /// <summary>
        /// Tipo de transação: Débito ou crédito
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Data da transação no formato YYYYMMDDHHMMSS
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Valor (será negativo quando a transação for debito
        /// </summary>
        public decimal Valor { get; set; }
        /// <summary>
        /// Descrição da transção
        /// </summary>
        public string Descricao { get; set; }

    }
}

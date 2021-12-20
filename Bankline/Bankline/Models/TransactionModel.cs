using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Models
{
    public class TransactionModel : Entity
    {
        public string Type { get; set; }
       
        public DateTime Date { get; set; }
       
        public decimal Valor { get; set; }

        public string Descricao { get; set; }
    }
}

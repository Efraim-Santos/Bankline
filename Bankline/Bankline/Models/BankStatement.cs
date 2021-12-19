using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bankline.Models
{
    public class BankStatement
    {
        public List<Transaction> Transacoes { get; set; }
        public BankStatement()
        {
            Transacoes = new List<Transaction>();
        }
    }
}
